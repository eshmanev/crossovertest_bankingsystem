using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BankingSystem.Domain;
using BankingSystem.LogicTier;
using BankingSystem.WebPortal.Controllers;
using BankingSystem.WebPortal.Models;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace BankingSystem.UnitTests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        private AccountController _controller;
        private Mock<IJournalService> _journalService;
        private Mock<IAccountService> _accountService;
        private Mock<ICustomerService> _customerService;
        private Mock<ICustomer> _currentCustomer;

        [SetUp]
        public void Setup()
        {
            _customerService = new Mock<ICustomerService>();
            _accountService = new Mock<IAccountService>();
            _journalService = new Mock<IJournalService>();
            _controller = new AccountController(_customerService.Object, _accountService.Object, _journalService.Object);

            _currentCustomer = new Mock<ICustomer>();
            _customerService.Setup(x => x.FindCustomerById(It.IsAny<int>())).Returns(_currentCustomer.Object);
        }

        [Test]
        public void ShouldGetView()
        {
            _controller.Index().ShouldBeOfType<ViewResult>();
        }

        [Test]
        public void ShouldGetAccounts()
        {
            // arrange
            var accounts = new List<IAccount>
            {
                Mock.Of<IAccount>(x => x.AccountNumber == "1" &&
                                       x.Currency == "USD" &&
                                       x.Balance == 100 &&
                                       x.BankCard == Mock.Of<IBankCard>(c => c.CardNumber == "12311" && c.CardHolder == "EVGENY" && c.ExpirationMonth == 10 && c.ExpirationYear == 2020))
            };
            _currentCustomer.SetupGet(x => x.Accounts).Returns(accounts);

            // act
            var result = _controller.GetAccounts();

            // assert
            var model = result.Data.ShouldBeAssignableTo<IEnumerable<AccountViewModel>>().ToList();
            model.Count.ShouldBe(accounts.Count);
            for (var i = 0; i < accounts.Count; i++)
            {
                model[i].AccountNumber.ShouldBe(accounts[i].AccountNumber);
                model[i].Currency.ShouldBe(accounts[i].Currency);
                model[i].Balance.ShouldBe(accounts[i].Balance);
                model[i].CardNumber.ShouldBe(accounts[i].BankCard.CardNumber);
                model[i].CardExpiration.ShouldBe($"{accounts[i].BankCard.ExpirationMonth} / {accounts[i].BankCard.ExpirationYear}");
                model[i].CardHolder.ShouldBe(accounts[i].BankCard.CardHolder);
            }
        }

        [Test]
        [TestCase("33333", "22222", 500, "Invalid source account")]
        [TestCase("11111", "33333", 500, "Invalid destination account")]
        [TestCase("11111", "22222", 0, "Amount must be greater than zero")]
        [TestCase("11111", "22222", 5000, "No enough money to transfer")]
        public void ShouldValidateViewModelOnSubmit(string sourceAcc, string descAcc, decimal amount, string expectedError)
        {
            // arrange
            var accounts = new List<IAccount>
            {
                Mock.Of<IAccount>(x => x.AccountNumber == "11111" && x.Balance == 3000),
                Mock.Of<IAccount>(x => x.AccountNumber == "22222" && x.Balance == 2000)
            };
            _currentCustomer.Setup(x => x.Accounts).Returns(accounts);
            var viewModel = new TransferViewModel
            {
                SourceAccount = sourceAcc,
                DestAccount =  descAcc,
                Amount = amount
            };

            // act
            var result = _controller.TransferToMyAccount(viewModel).Result;

            // assert
            var json = result.ShouldBeOfType<JsonResult>();
            var errors = json.Data.ShouldBeOfType<ErrorViewModel>();
            errors.Details.ShouldContain(x => x.Value == expectedError);
        }

        [Test]
        [TestCase("11111", "22222", 500)]
        public void ShouldTransferToMyAccount(string sourceAcc, string descAcc, decimal amount)
        {
            // arrange
            var accounts = new List<IAccount>
            {
                Mock.Of<IAccount>(x => x.AccountNumber == sourceAcc && x.Balance == 3000),
                Mock.Of<IAccount>(x => x.AccountNumber == descAcc && x.Balance == 2000)
            };
            _currentCustomer.Setup(x => x.Accounts).Returns(accounts);
            var viewModel = new TransferViewModel
            {
                SourceAccount = sourceAcc,
                DestAccount = descAcc,
                Amount = amount
            };

            // act
            var result = _controller.TransferToMyAccount(viewModel).Result;

            // assert
            _accountService.Verify(
                x => x.TransferMoney(
                    accounts[0], accounts[1], amount,
                    AmountConversionMode.SourceToTarget,
                    $"Internal transfer between your accounts from {sourceAcc} to {descAcc}."));
            var json = result.ShouldBeOfType<JsonResult>();
            json.Data.ShouldBeOfType<SuccessViewModel>()
                .Message.ShouldBe("The operation has successfully completed");
        }

        [Test]
        [TestCase("11111", "22222", 500)]
        public void ShouldTransferToOtherAccount(string sourceAcc, string descAcc, decimal amount)
        {
            // arrange
            var sourceAccount = Mock.Of<IAccount>(x => x.AccountNumber == sourceAcc && x.Balance == 3000);
            var destinationAccount = Mock.Of<IAccount>(x => x.AccountNumber == descAcc && x.Balance == 2000);
            _currentCustomer.Setup(x => x.Accounts).Returns(new List<IAccount> {sourceAccount});
            _accountService.Setup(x => x.FindAccount(descAcc)).Returns(destinationAccount);
            var viewModel = new TransferViewModel
            {
                SourceAccount = sourceAcc,
                DestAccount = descAcc,
                Amount = amount
            };

            // act
            var result = _controller.TransferToOtherAccount(viewModel).Result;

            // assert
            _accountService.Verify(
                x => x.TransferMoney(
                    sourceAccount, destinationAccount, amount,
                    AmountConversionMode.SourceToTarget,
                    $"Extenal transfer from account {sourceAcc} to account {descAcc}."));
            var json = result.ShouldBeOfType<JsonResult>();
            json.Data.ShouldBeOfType<SuccessViewModel>()
                .Message.ShouldBe("The operation has successfully completed");
        }
    }
}