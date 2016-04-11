using System;
using System.Linq;
using System.Web.Mvc;
using BankingSystem.Domain.Impl;
using BankingSystem.LogicTier;
using BankingSystem.WebPortal.Controllers;
using BankingSystem.WebPortal.Models;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace BankingSystem.UnitTests.Controllers
{
    [TestFixture]
    public class OnlinePaymentControllerTests
    {
        private OnlinePaymentController _controller;
        private Mock<IMerchantService> _merchantService;
        private Mock<IBankCardService> _bankCardService;
        private Mock<IAccountService> _accountService;

        [SetUp]
        public void Setup()
        {
            _merchantService = new Mock<IMerchantService>();
            _accountService = new Mock<IAccountService>();
            _bankCardService = new Mock<IBankCardService>();
            _controller = new OnlinePaymentController(_merchantService.Object, _bankCardService.Object, _accountService.Object);
        }

        [Test]
        public void ShouldRedirectOnFirstCall()
        {
            // arrange
            var merchantId = Guid.NewGuid();
            var currency = "USD";
            var sum = 100;
            var redirectUrl = "http://test.com";

            // act
            var result = _controller.Index(merchantId, currency, sum, redirectUrl);

            // assert
            result.ShouldBeOfType<RedirectToRouteResult>();
            var model = _controller.TempData["ViewModel"].ShouldBeOfType<OnlinePaymentViewModel>();
            model.RedirectUrl.ShouldBe(redirectUrl);
            model.MerchantId.ShouldBe(merchantId);
            model.Amount.ShouldBe(sum);
            model.Currency.ShouldBe(currency);
        }

        [Test]
        public void ShouldShowView_Success()
        {
            // arrange
            var model = new OnlinePaymentViewModel();
            _controller.TempData["ViewModel"] = model;

            // act
            var result = _controller.Payment();

            // assert
            result.ShouldBeOfType<ViewResult>().Model.ShouldBe(model);
        }


        [Test]
        public void ShouldShowView_NotFound()
        {
            // arrange
            _controller.TempData["ViewModel"] = null;

            // act
            var result = _controller.Payment();

            // assert
            result.ShouldBeOfType<HttpNotFoundResult>();
        }

        [Test]
        [TestCase("11111111-117B-4D60-8239-39A42A94ADC9", "USD", "EVGENY SHMANEV", "12345", "123", 1, 2020, "Invalid merchant")]
        [TestCase("E5310F8F-117B-4D60-8239-39A42A94ADC9", "EUR", "EVGENY SHMANEV", "12345", "123", 1, 2020, "Invalid merchant currency")]
        [TestCase("E5310F8F-117B-4D60-8239-39A42A94ADC9", "USD", null, "12345", "123", 1, 2020, "Cardholder name is required")]
        [TestCase("E5310F8F-117B-4D60-8239-39A42A94ADC9", "USD", "EVGENY SHMANEV", null, "123", 1, 2020, "Card number is required")]
        [TestCase("E5310F8F-117B-4D60-8239-39A42A94ADC9", "USD", "EVGENY SHMANEV", "12345", null, 1, 2020, "Security code is required")]
        [TestCase("E5310F8F-117B-4D60-8239-39A42A94ADC9", "USD", "EVGENY SHMANEV", "12345", "123", 0, 2020, "Invalid month")]
        [TestCase("E5310F8F-117B-4D60-8239-39A42A94ADC9", "USD", "EVGENY SHMANEV", "12345", "123", 1, 1850, "Invalid year")]
        [TestCase("E5310F8F-117B-4D60-8239-39A42A94ADC9", "USD", "EVGENY SHMANEV", "invalid card", "123", 1, 2020, "Invalid card number")]
        [TestCase("E5310F8F-117B-4D60-8239-39A42A94ADC9", "USD", "ANATOLY SOBCHAK", "12345", "123", 1, 2020, "Invalid cardholder name")]
        [TestCase("E5310F8F-117B-4D60-8239-39A42A94ADC9", "USD", "EVGENY SHMANEV", "12345", "123", 8, 2020, "Invalid month")]
        [TestCase("E5310F8F-117B-4D60-8239-39A42A94ADC9", "USD", "EVGENY SHMANEV", "12345", "123", 1, 2015, "Invalid year")]
        [TestCase("E5310F8F-117B-4D60-8239-39A42A94ADC9", "USD", "EVGENY SHMANEV", "12345", "invalid code", 1, 2020, "Invalid security code")]
        public void ShouldValidateViewModelOnSubmit(string merchantId, string currency, string cardHolderName, string cardNumber, string securityCode, int monthExpired, int yearExpired, string expectedError)
        {
            // arrange
            var model = new OnlinePaymentViewModel
            {
                MerchantId = new Guid(merchantId),
                Amount = 1000,
                Currency = currency,
                RedirectUrl = "http://test.com",
                CardHolderName = cardHolderName,
                CardNumber = cardNumber,
                SecurityCode = securityCode,
                MonthExpired = monthExpired,
                YearExpired = yearExpired
            };
            var bankCard = new BankCard("123", "0000", "EVGENY SHMANEV", "12345", 1, 2020);
            _bankCardService.Setup(x => x.FindBankCard("12345")).Returns(bankCard);

            var merchant = new Merchant("merchant", "test@com", "pass", new Guid("E5310F8F-117B-4D60-8239-39A42A94ADC9"), "store", "contact");
            var account = merchant.AddAccount("9876543210", "USD");
            _merchantService.Setup(x => x.FindMerchant(merchant.MerchantId)).Returns(merchant);

            // act
            var result = _controller.Payment(model).Result;

            // assert
            result.ShouldBeOfType<ViewResult>();
            _controller.ModelState.IsValid.ShouldBeFalse();
            _controller.ModelState.ShouldContain(x => x.Value.Errors.Any(error => error.ErrorMessage == expectedError));
        }

        [Test]
        [TestCase("E5310F8F-117B-4D60-8239-39A42A94ADC9", "USD", "EVGENY SHMANEV", "12345", "123", 1, 2020, "Invalid merchant")]
        public void ShouldRedirectToStoreAfterPayment(string merchantId, string currency, string cardHolderName, string cardNumber, string securityCode, int monthExpired, int yearExpired, string expectedError)
        {
            // arrange
            var model = new OnlinePaymentViewModel
            {
                MerchantId = new Guid(merchantId),
                Amount = 1000,
                Currency = currency,
                RedirectUrl = "http://test.com",
                CardHolderName = cardHolderName,
                CardNumber = cardNumber,
                SecurityCode = securityCode,
                MonthExpired = monthExpired,
                YearExpired = yearExpired
            };

            var customer = new Individual("customer", "test@com", "pass", "Evgeny", "Shmanev");
            var customerAccount = customer.AddAccount("11111111", "EUR");
            customerAccount.BankCard = new BankCard("123", "0000", "EVGENY SHMANEV", "12345", 1, 2020);
            _bankCardService.Setup(x => x.FindBankCard("12345")).Returns(customerAccount.BankCard);

            var merchant = new Merchant("merchant", "test@com", "pass", new Guid("E5310F8F-117B-4D60-8239-39A42A94ADC9"), "store", "contact");
            var merchantAccount = merchant.AddAccount("222222222", "USD");
            _merchantService.Setup(x => x.FindMerchant(merchant.MerchantId)).Returns(merchant);

            // act
            var result = _controller.Payment(model).Result;

            // assert
            _controller.ModelState.IsValid.ShouldBeTrue();
            _accountService.Verify(x => x.TransferMoney(customerAccount, merchantAccount, model.Amount, AmountConversionMode.TargetToSource, $"Online payment on {merchant.MerchantName}."));
            result.ShouldBeOfType<RedirectResult>()
                .Url.ShouldBe("http://test.com?status=success");
        }
    }
}