using System;
using System.Text.RegularExpressions;
using BankingSystem.ATM.Services;
using BankingSystem.IntegrationTests.Environment.Services;
using NUnit.Framework;
using Shouldly;

namespace BankingSystem.IntegrationTests.WebApiTests
{
    [TestFixture]
    public class BankingServiceProxyTests : WebApiTestsBase
    {
        private BankingServiceProxy _proxy;

        [SetUp]
        public void Setup()
        {
            _proxy = new TestBankingServiceProxy(Client);
        }

        [Test]
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, true)]
        [TestCase(TestVars.ValidCardNumber, "9517", false)]
        [TestCase("2111222233335", TestVars.ValidPinCode, false)]
        public async void ShouldValidatePin(string cardNumber, string pinCode, bool expectedResult)
        {
            // act
            var result = await _proxy.ValidatePin(cardNumber, pinCode);

            // assert
            result.ShouldBe(expectedResult);
        }

        [Test]
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, "9812", true)]
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, "invalid pin", false)]
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, "12345", false)]
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, "12", false)]
        [TestCase(TestVars.ValidCardNumber, "9517", TestVars.ValidPinCode, false)]
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
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, -10, null, ExpectedException = typeof (ArgumentException))]
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, 1, null)]
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, int.MaxValue, "Account balance exeeds limits.")]
        public async void ShouldWithdraw(string cardNumber, string pinCode, decimal amount, string expectedResult)
        {
            // act
            var result = await _proxy.Withdraw(cardNumber, pinCode, amount);

            // assert
            result.ShouldBe(expectedResult);
        }

        [Test]
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, false)]
        [TestCase(TestVars.ValidCardNumber, "12", true)]
        [TestCase("123", TestVars.ValidPinCode, true)]
        public async void ShouldGetBalance(string cardNumber, string pinCode, bool shouldBeEmpty)
        {
            // act
            var result = await _proxy.GetBalance(cardNumber, pinCode);

            // assert
            if (shouldBeEmpty)
                result.ShouldBeEmpty();
            else
            {
                var isMatch = Regex.IsMatch(result, @"([0-9]+[\s,])+" + TestVars.ValidCardCurrency);
                isMatch.ShouldBe(true);
            }
        }
    }
}