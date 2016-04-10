using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using BankingSystem.Messages;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;

namespace BankingSystem.IntergrationTests.WebApiTests
{
    [TestFixture]
    public class ApiBankCardTests : WebApiTestFixture
    {
        private const string ValidCardNumber = "2111222233335555";
        private const string ValidCardCurrency = "JPY";
        private const string ValidPinCode = "0000";

        [Test]
        [TestCase(ValidCardNumber, ValidPinCode, HttpStatusCode.OK)]
        [TestCase(ValidCardNumber, "9517", HttpStatusCode.Forbidden)]
        [TestCase("2111222233335", ValidPinCode, HttpStatusCode.Forbidden)]
        public async void ShouldCheckPinCode(string cardNumber, string pinCode, HttpStatusCode statusCode)
        {
            // act
            var result = await Client.GetAsync($"api/bankcard/{cardNumber}/pin/{pinCode}");

            // assert
            result.StatusCode.ShouldBe(statusCode);
        }

        [Test]
        [TestCase(ValidCardNumber, ValidPinCode, "9812", HttpStatusCode.OK)]
        [TestCase(ValidCardNumber, ValidPinCode, "invalid pin", HttpStatusCode.Forbidden)]
        [TestCase(ValidCardNumber, ValidPinCode, "12345", HttpStatusCode.Forbidden)]
        [TestCase(ValidCardNumber, ValidPinCode, "12", HttpStatusCode.Forbidden)]
        [TestCase(ValidCardNumber, "9517", ValidPinCode, HttpStatusCode.Forbidden)]
        public async void ShouldUpdatePinCode(string cardNumber, string pinCode, string newPin, HttpStatusCode statusCode)
        {
            // act
            var result = await Client.PutAsync($"api/bankcard/{cardNumber}/pin/{pinCode}", Serialize(new NewPinMessage {NewPin = newPin}));

            // assert
            result.StatusCode.ShouldBe(statusCode);
        }

        [Test]
        [TestCase(ValidCardNumber, ValidPinCode, 1, HttpStatusCode.OK)]
        [TestCase(ValidCardNumber, "123", 1, HttpStatusCode.Forbidden)]
        [TestCase("123", ValidPinCode, 1, HttpStatusCode.Forbidden)]
        [TestCase(ValidCardNumber, ValidPinCode, -int.MaxValue, HttpStatusCode.Forbidden)]
        public async void ShouldUpdateBalance(string cardNumber, string pinCode, decimal amount, HttpStatusCode expectedResult)
        {
            // act
            var result = await Client.PutAsync($"api/bankcard/{cardNumber}/pin/{pinCode}/balance", Serialize(new ChangeAmountMessage {Amount = amount, Description = "API Integration Test"}));

            // assert
            result.StatusCode.ShouldBe(expectedResult);
        }

        [Test]
        [TestCase(ValidCardNumber, ValidPinCode, HttpStatusCode.OK, false)]
        [TestCase(ValidCardNumber, "12", HttpStatusCode.Forbidden, true)]
        [TestCase("123", ValidPinCode, HttpStatusCode.Forbidden, true)]
        public async void ShouldGetBalance(string cardNumber, string pinCode, HttpStatusCode expectedResult, bool shouldBeNull)
        {
            // act
            var result = await Client.GetAsync($"api/bankcard/{cardNumber}/pin/{pinCode}/balance");
            var content = result.Content?.ReadAsStringAsync().Result;

            // assert
            result.StatusCode.ShouldBe(expectedResult);
            if (shouldBeNull)
                content.ShouldBeNull();
            else
            {
                var isMatch = Regex.IsMatch(content, @"([0-9]+[\s,])+" + ValidCardCurrency);
                isMatch.ShouldBe(true);
            }
        }

        private StringContent Serialize<T>(T data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.Unicode, "application/json");
        }
    }
}