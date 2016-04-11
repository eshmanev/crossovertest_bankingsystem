using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BankingSystem.Domain;
using BankingSystem.LogicTier;
using BankingSystem.WebPortal.Models;
using log4net;
using Microsoft.AspNet.Identity;

namespace BankingSystem.WebPortal.Controllers
{
    /// <summary>
    ///     Represents an controller which provides functionality for a page with user's accounts.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    [RequireHttps]
    public class AccountController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (AccountController));
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly IJournalService _journalService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="customerService">The customer service.</param>
        /// <param name="accountService">The account service.</param>
        /// <param name="journalService">The journal service.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public AccountController(ICustomerService customerService, IAccountService accountService, IJournalService journalService)
        {
            if (customerService == null)
                throw new ArgumentNullException(nameof(customerService));

            if (accountService == null)
                throw new ArgumentNullException(nameof(accountService));

            if (journalService == null)
                throw new ArgumentNullException(nameof(journalService));

            _customerService = customerService;
            _accountService = accountService;
            _journalService = journalService;
        }


        /// <summary>
        ///     Returns the Accounts view.
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Returns a list of accounts for the current user.
        /// </summary>
        public JsonResult GetAccounts()
        {
            var customer = GetCurrentCustomer();
            var model = customer.Accounts.Select(x => new AccountViewModel
            {
                AccountNumber = x.AccountNumber,
                Currency = x.Currency,
                Balance = x.Balance,
                CardNumber = x.BankCard?.CardNumber,
                CardExpiration = x.BankCard != null ? $"{x.BankCard.ExpirationMonth} / {x.BankCard.ExpirationYear}" : string.Empty,
                CardHolder = x.BankCard?.CardHolder
            });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Gets a list of journals for the current user.
        /// </summary>
        public JsonResult GetJournals()
        {
            var customer = GetCurrentCustomer();
            var journals = _journalService.GetCustomerJournals(customer.Id);
            var model = journals.OrderByDescending(x => x.DateTimeCreated).Select(x => new JournalViewModel
            {
                DateCreated = x.DateTimeCreated.ToLocalTime().ToString("MM/dd/yyyy HH:mm"),
                Description = x.Description
            });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Makes an account-to-account transfer.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public async Task<JsonResult> TransferToMyAccount(TransferViewModel viewModel)
        {
            var customer = GetCurrentCustomer();

            // search for logged user's accounts.
            var sourceAccount = customer.Accounts.SingleOrDefault(x => x.AccountNumber == viewModel.SourceAccount);
            var destAccount = customer.Accounts.SingleOrDefault(x => x.AccountNumber == viewModel.DestAccount);

            return await MakeTransfer(viewModel, sourceAccount, destAccount, true);
        }

        /// <summary>
        ///     Makes an account-to-account transfer.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public async Task<JsonResult> TransferToOtherAccount(TransferViewModel viewModel)
        {
            var customer = GetCurrentCustomer();

            // search fo logged user's account and other customer's account.
            var sourceAccount = customer.Accounts.SingleOrDefault(x => x.AccountNumber == viewModel.SourceAccount);
            var destAccount = _accountService.FindAccount(viewModel.DestAccount);

            return await MakeTransfer(viewModel, sourceAccount, destAccount, false);
        }

        /// <summary>
        ///     Gets the current customer.
        /// </summary>
        /// <returns>A customer.</returns>
        private ICustomer GetCurrentCustomer()
        {
            var userId = User?.Identity?.GetUserId<int>() ?? 0;
            var customer = _customerService.FindCustomerById(userId);
            Debug.Assert(customer != null);
            return customer;
        }

        /// <summary>
        ///     Makes the transfer.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="sourceAccount">The source account.</param>
        /// <param name="destAccount">The dest account.</param>
        /// <param name="internalTransfer"><c>true</c> if it's internal transfer; otherwise false.</param>
        /// <returns></returns>
        private async Task<JsonResult> MakeTransfer(TransferViewModel viewModel, IAccount sourceAccount, IAccount destAccount, bool internalTransfer)
        {
            if (sourceAccount == null)
                ModelState.AddModelError("SourceAccount", "Invalid source account");

            if (destAccount == null)
                ModelState.AddModelError("DestAccount", "Invalid destination account");

            if (viewModel.Amount <= 0)
                ModelState.AddModelError("Amount", "Amount must be greater than zero");

            if (sourceAccount != null && sourceAccount.Balance < viewModel.Amount)
                ModelState.AddModelError("Amount", "No enough money to transfer");

            if (!ModelState.IsValid)
                return this.JsonError();

            var description = internalTransfer
                ? $"Internal transfer between your accounts from {sourceAccount.AccountNumber} to {destAccount.AccountNumber}."
                : $"Extenal transfer from account {sourceAccount.AccountNumber} to account {destAccount.AccountNumber}.";

            try
            {
                await _accountService.TransferMoney(sourceAccount, destAccount, viewModel.Amount, AmountConversionMode.SourceToTarget,  description);
                return this.JsonSuccess();
            }
            catch (Exception ex)
            {
                Log.Error($"An unpexpected error has occurred while transferring money from account {viewModel.SourceAccount} to account {viewModel.DestAccount}", ex);
                return this.JsonError("An unexpected error has occurred");
            }
        }
    }
}