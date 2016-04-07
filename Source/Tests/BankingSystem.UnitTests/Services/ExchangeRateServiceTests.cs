using BankingSystem.LogicTier.Impl;
using NUnit.Framework;
using Shouldly;

namespace BankingSystem.UnitTests.Services
{
    [TestFixture]
    public class ExchangeRateServiceTests
    {
        private ExchangeRateService _service;

        [SetUp]
        public void Setup()
        {
            _service = new ExchangeRateService();
        }

        [Test]
        [Category("Integration Tests")]
        public async void ShouldGetExhangeRateAsync()
        {
            // act
            var result = await _service.GetExhangeRateAsync("USD", "EUR");

            // assert
            result.ShouldBeGreaterThan(0);
            result.ShouldBeLessThan(2);
        }
    }
}