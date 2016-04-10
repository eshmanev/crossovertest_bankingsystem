using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using BankingSystem.Messages;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;

namespace BankingSystem.IntegrationTests.WebApiTests
{
    [TestFixture]
    public class ApiBankCardTests : WebApiTestsBase
    {
        [Test]
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, HttpStatusCode.OK)]
        [TestCase(TestVars.ValidCardNumber, "9517", HttpStatusCode.Forbidden)]
        [TestCase("2111222233335", TestVars.ValidPinCode, HttpStatusCode.Forbidden)]
        public async void ShouldCheckPinCode(string cardNumber, string pinCode, HttpStatusCode statusCode)
        {
            // act
            var result = await Client.GetAsync($"api/bankcard/{cardNumber}/pin/{pinCode}");

            // assert
            result.StatusCode.ShouldBe(statusCode);
        }

        [Test]
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, "9812", HttpStatusCode.OK)]
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, "invalid pin", HttpStatusCode.Forbidden)]
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, "12345", HttpStatusCode.Forbidden)]
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, "12", HttpStatusCode.Forbidden)]
        [TestCase(TestVars.ValidCardNumber, "9517", TestVars.ValidPinCode, HttpStatusCode.Forbidden)]
        public async void ShouldUpdatePinCode(string cardNumber, string pinCode, string newPin, HttpStatusCode statusCode)
        {
            // act
            var result = await Client.PutAsync($"api/bankcard/{cardNumber}/pin/{pinCode}", Serialize(new NewPinMessage {NewPin = newPin}));

            // assert
            result.StatusCode.ShouldBe(statusCode);

            // cleanup
            await Client.PutAsync($"api/bankcard/{cardNumber}/pin/{newPin}", Serialize(new NewPinMessage { NewPin = pinCode }));
        }

        [Test]
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, 1, HttpStatusCode.OK)]
        [TestCase(TestVars.ValidCardNumber, "123", 1, HttpStatusCode.Forbidden)]
        [TestCase("123", TestVars.ValidPinCode, 1, HttpStatusCode.Forbidden)]
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, -int.MaxValue, HttpStatusCode.Forbidden)]
        public async void ShouldUpdateBalance(string cardNumber, string pinCode, decimal amount, HttpStatusCode expectedResult)
        {
            // act
            var result = await Client.PutAsync($"api/bankcard/{cardNumber}/pin/{pinCode}/balance", Serialize(new ChangeAmountMessage {Amount = amount, Description = "API Integration Test"}));

            // assert
            result.StatusCode.ShouldBe(expectedResult);
        }

        [Test]
        [TestCase(TestVars.ValidCardNumber, TestVars.ValidPinCode, HttpStatusCode.OK, false)]
        [TestCase(TestVars.ValidCardNumber, "12", HttpStatusCode.Forbidden, true)]
        [TestCase("123", TestVars.ValidPinCode, HttpStatusCode.Forbidden, true)]
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
                var isMatch = Regex.IsMatch(content, @"([0-9]+[\s,])+" + TestVars.ValidCardCurrency);
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