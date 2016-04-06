using System;
using System.Linq;
using System.Web.Mvc;
using BankingSystem.Domain;
using BankingSystem.WebPortal.Models;
using log4net;
using Microsoft.AspNet.Identity;

namespace BankingSystem.WebPortal.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (AccountController));
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;

        public AccountController(ICustomerService customerService, IAccountService accountService)
        {
            if (customerService == null)
                throw new ArgumentNullException(nameof(customerService));

            if (accountService == null)
                throw new ArgumentNullException(nameof(accountService));

            _customerService = customerService;
            _accountService = accountService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {
            var userId = User.Identity.GetUserId<int>();
            var customer = _customerService.FindCustomerById(userId);
            return Json(customer.Accounts, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Transfer(TransferViewModel viewModel)
        {
            var userId = User.Identity.GetUserId<int>();
            var customer = _customerService.FindCustomerById(userId);

            var sourceAccount = customer.Accounts.SingleOrDefault(x => x.AccountNumber == viewModel.SourceAccount);
            var destAccount = customer.Accounts.SingleOrDefault(x => x.AccountNumber == viewModel.DestAccount);

            if (sourceAccount == null)
                ModelState.AddModelError("SourceAccount", "Invalid source account");

            if (destAccount == null)
                ModelState.AddModelError("DestAccount", "Invalid destination account");

            if (viewModel.Amount <= 0)
                ModelState.AddModelError("Amount", "Amount must be greater than zero");

            if (sourceAccount != null && viewModel.Amount < sourceAccount.Balance)
                ModelState.AddModelError("Amount", "No enough money to transfer");

            if (!ModelState.IsValid)
                return this.JsonError();

            try
            {
                _accountService.TransferMoney(sourceAccount, destAccount, viewModel.Amount);
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