using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BankingSystem.Common.Data;
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

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="customerService">The customer service.</param>
        /// <param name="accountService">The account service.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public AccountController(ICustomerService customerService, IAccountService accountService)
        {
            if (customerService == null)
                throw new ArgumentNullException(nameof(customerService));

            if (accountService == null)
                throw new ArgumentNullException(nameof(accountService));

            _customerService = customerService;
            _accountService = accountService;
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
        public JsonResult Get()
        {
            var userId = User.Identity.GetUserId<int>();
            var customer = _customerService.FindCustomerById(userId);
            var model = customer.Accounts.Select(x => new AccountViewModel {AccountNumber = x.AccountNumber, Currency = x.Currency, Balance = x.Balance});
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Makes an account-to-account transfer.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public async Task<JsonResult> TransferToMyAccount(TransferViewModel viewModel)
        {
            var userId = User.Identity.GetUserId<int>();
            var customer = _customerService.FindCustomerById(userId);

            // search for logged user's accounts.
            var sourceAccount = customer.Accounts.SingleOrDefault(x => x.AccountNumber == viewModel.SourceAccount);
            var destAccount = customer.Accounts.SingleOrDefault(x => x.AccountNumber == viewModel.DestAccount);

            return await MakeTransfer(viewModel, sourceAccount, destAccount);
        }

        /// <summary>
        ///     Makes an account-to-account transfer.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public async Task<JsonResult> TransferToOtherAccount(TransferViewModel viewModel)
        {
            var userId = User.Identity.GetUserId<int>();
            var customer = _customerService.FindCustomerById(userId);

            // search fo logged user's account and other customer's account.
            var sourceAccount = customer.Accounts.SingleOrDefault(x => x.AccountNumber == viewModel.SourceAccount);
            var destAccount = _accountService.FindAccount(viewModel.DestAccount);

            return await MakeTransfer(viewModel, sourceAccount, destAccount);
        }

        private async Task<JsonResult> MakeTransfer(TransferViewModel viewModel, IAccount sourceAccount, IAccount destAccount)
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

            try
            {
                await _accountService.TransferMoney(sourceAccount, destAccount, viewModel.Amount);
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