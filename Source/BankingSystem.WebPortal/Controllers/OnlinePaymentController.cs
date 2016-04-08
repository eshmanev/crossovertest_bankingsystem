using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BankingSystem.Common.Data;
using BankingSystem.LogicTier;
using BankingSystem.LogicTier.Impl;
using BankingSystem.WebPortal.Models;
using log4net;

namespace BankingSystem.WebPortal.Controllers
{
    /// <summary>
    ///     Represents a controller of online payments.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    /// <remarks>
    ///     To integrate online payment facility for this bank an external website
    ///     should add a link in the following format:
    ///     https://locahost:44300/onlinepayment/5D9168B6-A03B-412E-A5D9-1AC5C5105BA9/USD/1000?redirectUrl=url
    ///     where:
    ///     5D9168B6-A03B-412E-A5D9-1AC5C5105BA9 is a GUID which identifies the merchant.
    ///     USD represents a currency of the merchant's account to credit. Merchant may contain few accounts with different
    ///     currencies.
    ///     1000 is a payment sum.
    ///     redirectUrl represents an URL on which the request should be redirected when payment is submitted.
    ///     Using this link a request will be redirected to the bank's payment page
    ///     where user can enter a bank card number and perform a payment.
    /// </remarks>
    [RequireHttps]
    public class OnlinePaymentController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (OnlinePaymentController));
        private readonly IMerchantService _merchantService;
        private readonly IBankCardService _bankCardService;
        private readonly IAccountService _accountService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="OnlinePaymentController" /> class.
        /// </summary>
        /// <param name="merchantService">The merchant service.</param>
        /// <param name="bankCardService">The bank card service.</param>
        /// <param name="accountService">The account service.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public OnlinePaymentController(IMerchantService merchantService, IBankCardService bankCardService, IAccountService accountService)
        {
            if (merchantService == null)
                throw new ArgumentNullException(nameof(merchantService));

            if (bankCardService == null)
                throw new ArgumentNullException(nameof(bankCardService));

            if (accountService == null)
                throw new ArgumentNullException(nameof(accountService));

            _merchantService = merchantService;
            _bankCardService = bankCardService;
            _accountService = accountService;
        }

        /// <summary>
        ///     Entry point for an online payment.
        /// </summary>
        /// <param name="merchant">The merchant.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="sum">The sum.</param>
        /// <param name="redirectUrl">The redirect url.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index(Guid merchant, string currency, decimal sum, string redirectUrl)
        {
            TempData["ViewModel"] = new OnlinePaymentViewModel
            {
                RedirectUrl = redirectUrl,
                MerchantId = merchant,
                Amount = sum,
                Currency = currency.ToUpper(),
                Months = GetMonths(),
                Years = GetYears()
            };
            return RedirectToAction("Payment");
        }

        [AllowAnonymous]
        public ActionResult Payment()
        {
            var viewModel = TempData["ViewModel"] as OnlinePaymentViewModel;
            return viewModel == null ? (ActionResult) HttpNotFound() : View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Payment(OnlinePaymentViewModel viewModel)
        {
            viewModel.Months = GetMonths();
            viewModel.Years = GetYears();

            // these values cannot be changed by user, so if they are changed we should return Bad Request
            if (string.IsNullOrWhiteSpace(viewModel.RedirectUrl) || viewModel.MerchantId == Guid.Empty ||
                viewModel.Amount <= 0 || string.IsNullOrWhiteSpace(viewModel.Currency))
            {
                return BadRequest();
            }

            // validate required parameters
            if (ModelState.IsValid)
                ValidateRequiredParameters(viewModel);

            // validate bank card
            IBankCard bankCard = null;
            if (ModelState.IsValid)
                ValidateBankCard(viewModel, out bankCard);

            IMerchant merchant = null;
            IAccount merchantAccount = null;
            if (ModelState.IsValid)
                ValidateMerchant(viewModel, out merchant, out merchantAccount);

            if (!ModelState.IsValid)
                return View(viewModel);

            Debug.Assert(bankCard != null);
            Debug.Assert(merchant != null);
            Debug.Assert(merchantAccount != null);

            try
            {
                var description = $"Online payment on {merchant.MerchantName}. Sum {viewModel.Amount} {bankCard.Account.Currency}";
                _accountService.TransferMoney(bankCard.Account, merchantAccount, viewModel.Amount, description);
            }
            catch (BankingServiceException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                Log.Error("Unexpected error has occurred while processing online payment", ex);
                ModelState.AddModelError(string.Empty, "Unexpected error has occurred. Please try again later.");
                return View(viewModel);
            }

            // redirect to merchant and pass Success status via query string
            var url = viewModel.RedirectUrl;
            url += url.Contains("?") ? "&status=success" : "?status=success";
            return Redirect(url);
        }

        private void ValidateMerchant(OnlinePaymentViewModel viewModel, out IMerchant merchant, out IAccount merchantAccount)
        {
            merchant = _merchantService.FindMerchant(viewModel.MerchantId);
            if (merchant == null)
            {
                merchantAccount = null;
                ModelState.AddModelError(string.Empty, "Invalid merchant");
            }
            else
            {
                merchantAccount = merchant.Accounts.FirstOrDefault(x => string.Equals(x.Currency, viewModel.Currency, StringComparison.InvariantCultureIgnoreCase));
                if (merchantAccount == null)
                    ModelState.AddModelError(string.Empty, "Invalid merchant currency");
            }
        }

        private void ValidateBankCard(OnlinePaymentViewModel viewModel, out IBankCard bankCard)
        {
            bankCard = _bankCardService.FindBankCard(viewModel.CardNumber);
            if (bankCard == null)
                ModelState.AddModelError(nameof(viewModel.CardNumber), "Invalid card number");
            else
            {
                if (!string.Equals(bankCard.CardHolder.ToUpper(), viewModel.CardHolderName, StringComparison.InvariantCulture))
                    ModelState.AddModelError(nameof(viewModel.CardHolderName), "Invalid cardholder name");

                if (!string.Equals(bankCard.CardNumber.ToUpper(), viewModel.CardNumber, StringComparison.InvariantCulture))
                    ModelState.AddModelError(nameof(viewModel.CardNumber), "Invalid card number");

                if (bankCard.ExpirationMonth != viewModel.MonthExpired)
                    ModelState.AddModelError(nameof(viewModel.MonthExpired), "Invalid month");

                if (bankCard.ExpirationYear != viewModel.YearExpired)
                    ModelState.AddModelError(nameof(viewModel.YearExpired), "Invalid year");

                if (!string.Equals(bankCard.CsvCode.ToUpper(), viewModel.SecurityCode, StringComparison.InvariantCulture))
                    ModelState.AddModelError(nameof(viewModel.SecurityCode), "Invalid security code");
            }
        }

        private void ValidateRequiredParameters(OnlinePaymentViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.CardHolderName))
                ModelState.AddModelError(nameof(viewModel.CardHolderName), "Cardholder name is required");

            if (string.IsNullOrWhiteSpace(viewModel.CardNumber))
                ModelState.AddModelError(nameof(viewModel.CardNumber), "Card number is required");

            if (string.IsNullOrWhiteSpace(viewModel.SecurityCode))
                ModelState.AddModelError(nameof(viewModel.SecurityCode), "Security code is required");

            if (!viewModel.Months.Contains(viewModel.MonthExpired))
                ModelState.AddModelError(nameof(viewModel.MonthExpired), "Invalid month");

            if (!viewModel.Years.Contains(viewModel.YearExpired))
                ModelState.AddModelError(nameof(viewModel.YearExpired), "Invalid year");
        }

        private ActionResult BadRequest()
        {
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return Content("Invalid request");
        }

        private List<int> GetMonths()
        {
            var result = new List<int>();
            for (var i = 1; i <= 12; i++)
                result.Add(i);
            return result;
        }

        private List<int> GetYears()
        {
            var result = new List<int>();
            for (var i = DateTime.Now.Year; i < DateTime.Now.Year + 10; i++)
                result.Add(i);
            return result;
        }
    }
}