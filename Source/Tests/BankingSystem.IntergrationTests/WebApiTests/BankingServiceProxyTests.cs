using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using BankingSystem.ATM;
using BankingSystem.ATM.Services;
using NUnit.Framework;
using Shouldly;

namespace BankingSystem.IntergrationTests.WebApiTests
{
    [TestFixture]
    public class BankingServiceProxyTests : WebApiTestFixture
    {
        // Account: 445641994546495. Currency: JPY.Card Holder: Anatoly Green
        private const string ValidCardNumber = "2111222233335555";
        private const string ValidCardCurrency = "JPY";
        private const string ValidPinCode = "0000";
        private BankingServiceProxy _proxy;

        [SetUp]
        public void Setup()
        {
            _proxy = new TestableBankingServiceProxy(Client);
        }

        [Test]
        [TestCase(ValidCardNumber, ValidPinCode, true)]
        [TestCase(ValidCardNumber, "9517", false)]
        [TestCase("2111222233335", ValidPinCode, false)]
        public async void ShouldValidatePin(string cardNumber, string pinCode, bool expectedResult)
        {
            // act
            var result = await _proxy.ValidatePin(cardNumber, pinCode);

            // assert
            result.ShouldBe(expectedResult);
        }

        [Test]
        [TestCase(ValidCardNumber, ValidPinCode, "9812", true)]
        [TestCase(ValidCardNumber, ValidPinCode, "invalid pin", false)]
        [TestCase(ValidCardNumber, ValidPinCode, "12345", false)]
        [TestCase(ValidCardNumber, ValidPinCode, "12", false)]
        [TestCase(ValidCardNumber, "9517", ValidPinCode, false)]
        public async void ShouldChangePin(string cardNumber, string pinCode, string newPin, bool expectedResult)
        {
            // act
            var result = await _proxy.ChangePin(cardNumber, pinCode, newPin);

            // assert
            result.ShouldBe(expectedResult);

            // cleanup
            if (result)
                await _proxy.ChangePin(cardNumber, newPin, pinCode);
        }

        [Test]
        [TestCase(ValidCardNumber, ValidPinCode, -10, null, ExpectedException = typeof (ArgumentException))]
        [TestCase(ValidCardNumber, ValidPinCode, 1, null)]
        [TestCase(ValidCardNumber, ValidPinCode, int.MaxValue, "Account balance exeeds limits.")]
        public async void ShouldWithdraw(string cardNumber, string pinCode, decimal amount, string expectedResult)
        {
            // act
            var result = await _proxy.Withdraw(cardNumber, pinCode, amount);

            // assert
            result.ShouldBe(expectedResult);
        }

        [Test]
        [TestCase(ValidCardNumber, ValidPinCode, false)]
        [TestCase(ValidCardNumber, "12", true)]
        [TestCase("123", ValidPinCode, true)]
        public async void ShouldGetBalance(string cardNumber, string pinCode, bool shouldBeEmpty)
        {
            // act
            var result = await _proxy.GetBalance(cardNumber, pinCode);

            // assert
            if (shouldBeEmpty)
                result.ShouldBeEmpty();
            else
            {
                var isMatch = Regex.IsMatch(result, @"([0-9]+[\s,])+" + ValidCardCurrency);
                isMatch.ShouldBe(true);
            }
        }

        private class TestableBankingServiceProxy : BankingServiceProxy
        {
            private readonly HttpClient _client;

            public TestableBankingServiceProxy(HttpClient client) : base(new Settings())
            {
                _client = client;
            }

            protected override IHttpClient CreateClient()
            {
                return new NonDisposableHttpClient(_client);
            }
        }
    }
}