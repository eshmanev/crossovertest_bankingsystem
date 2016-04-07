using System;
using BankingSystem.Common.Data;
using BankingSystem.LogicTier.Impl;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace BankingSystem.UnitTests.Services
{
    [TestFixture]
    public class BankCardServiceTests
    {
        private FakeDatabaseContext _context;
        private BankCardService _service;
        Mock<IBankCard> _bankCard;

        [SetUp]
        public void Setup()
        {
            _context = new FakeDatabaseContext();
            _service = new BankCardService(_context);

            _bankCard = new Mock<IBankCard>();
            _bankCard.SetupGet(x => x.CardNumber).Returns("112233");
            _bankCard.SetupGet(x => x.PinCode).Returns("1111");
            _context.BankCards.Setup(x => x.FindByNumber("112233")).Returns(_bankCard.Object);
        }

        [Test]
        [TestCase("112233", "1111", true)]
        [TestCase("112233", "1234", false)]
        [TestCase("111111", "1111", false)]
        public void ShouldCheckPin(string card, string pin, bool expected)
        {
            // act
            var result = _service.CheckPin(card, pin);

            // assert
            result.ShouldBe(expected);
        }

        [Test]
        [TestCase("112233")]
        [TestCase("111111", ExpectedException = typeof(ArgumentException))]
        public void ShouldUpdatePin(string card)
        {
            // arrange
            var newPin = "9876";

            // act
            _service.UpdatePin(card, newPin);

            // assert
            _bankCard.VerifySet(x => x.PinCode = newPin);
            _context.BankCards.Verify(x => x.Update(_bankCard.Object));
        }

        [Test]
        [TestCase("112233")]
        [TestCase("111111", ExpectedException = typeof(ArgumentException))]
        public void ShouldGetBalance(string card)
        {
            // arrange
            var account = Mock.Of<IAccount>(x => x.Balance == 100m && x.Currency == "USD");
            _bankCard.SetupGet(x => x.Account).Returns(account);
            
            // act
            var result = _service.GetBalance(card);

            // assert
            result.ShouldBe(100m.ToString("N2") + " USD");
        }

        [Test]
        public void ShouldFindBankCard()
        {
            // act and assert
            _service.FindBankCard("112233").ShouldBe(_bankCard.Object);
            _service.FindBankCard("111111").ShouldBeNull();
        }
    }
}