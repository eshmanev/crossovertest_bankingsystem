using System;
using System.Threading.Tasks;
using BankingSystem.Common.Data;
using BankingSystem.LogicTier;
using BankingSystem.LogicTier.Impl;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace BankingSystem.UnitTests.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        private AccountService _service;
        private FakeDatabaseContext _context;
        private Mock<IExchangeRateService> _exchangeRangeService;
        private Mock<IBankBalanceService> _bankBalanceService;

        [SetUp]
        public void Setup()
        {
            _context = new FakeDatabaseContext();
            _exchangeRangeService = new Mock<IExchangeRateService>();
            _bankBalanceService = new Mock<IBankBalanceService>();
            _service = new AccountService(_context, _exchangeRangeService.Object, _bankBalanceService.Object);
        }

        [Test]
        public async void ShouldTransferMoney_DifferentCurrencies()
        {
            // arrange
            IOperation insertedOperation = null;
            _context.Operations.Setup(x => x.Insert(It.IsAny<IOperation>()))
                .Callback((IOperation o) => insertedOperation = o);

            var amount = 100m;
            var sourceAccount = new Mock<IAccount>();
            var destAccount = new Mock<IAccount>();
            sourceAccount.SetupGet(x => x.Currency).Returns("USD");
            sourceAccount.SetupGet(x => x.Balance).Returns(1000m);
            destAccount.SetupGet(x => x.Currency).Returns("EUR");
            destAccount.SetupGet(x => x.Balance).Returns(2000m);
            _exchangeRangeService.Setup(x => x.GetExhangeRateAsync("USD", "EUR")).Returns(Task.FromResult(0.8m));

            // act
            await _service.TransferMoney(sourceAccount.Object, destAccount.Object, amount);

            // assert
            insertedOperation.ShouldNotBeNull();
            insertedOperation.DateTimeCreated.ShouldBeGreaterThan(DateTime.UtcNow.AddMinutes(-5));
            insertedOperation.Amount.ShouldBe(amount);
            insertedOperation.Commission.ShouldBe(Math.Round(amount*AccountService.DefaultCommission, 2));
            insertedOperation.Description.ShouldNotBeNullOrEmpty();

            sourceAccount.VerifySet(x => x.Balance = 900m);
            destAccount.VerifySet(x => x.Balance = 2000m + 78.4m); // (100 - commission) * exhchange rate

            _context.Accounts.Verify(x => x.Update(sourceAccount.Object));
            _context.Accounts.Verify(x => x.Update(destAccount.Object));
            _bankBalanceService.Verify(x => x.AddRevenue(2m, It.IsAny<string>())); // 2 USD commission
            _context.Transaction.Verify(x => x.Commit());
        }

        [Test]
        public async void ShouldTransferMoney_SameCurrencies()
        {
            // arrange
            IOperation insertedOperation = null;
            _context.Operations.Setup(x => x.Insert(It.IsAny<IOperation>()))
                .Callback((IOperation o) => insertedOperation = o);

            var amount = 100m;
            var sourceAccount = new Mock<IAccount>();
            var destAccount = new Mock<IAccount>();
            sourceAccount.SetupGet(x => x.Currency).Returns("USD");
            sourceAccount.SetupGet(x => x.Balance).Returns(1000m);
            destAccount.SetupGet(x => x.Currency).Returns("USD");
            destAccount.SetupGet(x => x.Balance).Returns(2000m);
            _exchangeRangeService.Setup(x => x.GetExhangeRateAsync("USD", "USD")).Returns(Task.FromResult(0.8m));

            // act
            await _service.TransferMoney(sourceAccount.Object, destAccount.Object, amount);

            // assert
            insertedOperation.ShouldNotBeNull();
            insertedOperation.DateTimeCreated.ShouldBeGreaterThan(DateTime.UtcNow.AddMinutes(-5));
            insertedOperation.Amount.ShouldBe(amount);
            insertedOperation.Commission.ShouldBe(0);
            insertedOperation.Description.ShouldNotBeNullOrEmpty();

            sourceAccount.VerifySet(x => x.Balance = 900m);
            destAccount.VerifySet(x => x.Balance = 2100m);

            _context.Accounts.Verify(x => x.Update(sourceAccount.Object));
            _context.Accounts.Verify(x => x.Update(destAccount.Object));
            _bankBalanceService.Verify(x => x.AddRevenue(It.IsAny<decimal>(), It.IsAny<string>()), Times.Never);
            _context.Transaction.Verify(x => x.Commit());
        }

        [Test]
        [ExpectedException(typeof(BankingServiceException))]
        public void ShouldUpdateBalance_ExceedLimits()
        {
            // arrange
            var sourceAccount = new Mock<IAccount>();
            sourceAccount.SetupGet(x => x.Balance).Returns(1000m);

            // act
            _service.UpdateBalance(sourceAccount.Object, -2000m);
        }

        [Test]
        public void ShouldUpdateBalance_Success()
        {
            // arrange
            var sourceAccount = new Mock<IAccount>();
            sourceAccount.SetupGet(x => x.Balance).Returns(1000m);

            // act
            _service.UpdateBalance(sourceAccount.Object, -100m);

            // assert
            sourceAccount.VerifySet(x => x.Balance = 900m);
            _context.Accounts.Verify(x => x.Update(sourceAccount.Object));
        }

        [Test]
        public void ShouldFindAccount()
        {
            // arrange
            var sourceAccount = new Mock<IAccount>();
            _context.Accounts.Setup(x => x.FindAccount("123")).Returns(sourceAccount.Object);

            // act
            var result = _service.FindAccount("123");

            // assert
            result.ShouldBe(sourceAccount.Object);
        }
    }
}