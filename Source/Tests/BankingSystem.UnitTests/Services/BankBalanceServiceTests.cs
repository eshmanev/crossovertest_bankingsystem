using BankingSystem.Domain;
using BankingSystem.LogicTier.Impl;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace BankingSystem.UnitTests.Services
{
    [TestFixture]
    public class BankBalanceServiceTests
    {
        private FakeDatabaseContext _context;
        private BankBalanceService _service;

        [SetUp]
        public void Setup()
        {
            _context = new FakeDatabaseContext();
            _service = new BankBalanceService(_context);
            _context.BankBalances.Setup(x => x.GetAll()).Returns(new IBankBalance[0]);
        }

        [Test]
        public void ShouldAddRevenue()
        {
            // arrange
            IBankBalance inserted = null;
            _context.BankBalances
                .Setup(x => x.Insert(It.IsAny<IBankBalance>()))
                .Callback((IBankBalance b) =>
                {
                    inserted = b;
                    inserted.TotalBalance = 1000;
                });

            // act
            _service.AddRevenue(100, "test");

            // assert
            inserted.ShouldNotBeNull();
            inserted.TotalBalance.ShouldBe(1100);
            _context.BankBalances.Verify(x => x.Update(inserted));
        }

        [Test]
        public void ShouldAddExpences()
        {
            // arrange
            IBankBalance inserted = null;
            _context.BankBalances
                .Setup(x => x.Insert(It.IsAny<IBankBalance>()))
                .Callback((IBankBalance b) =>
                {
                    inserted = b;
                    inserted.TotalBalance = 1000;
                });

            // act
            _service.AddExpences(100, "test");

            // assert
            inserted.ShouldNotBeNull();
            inserted.TotalBalance.ShouldBe(900);
            _context.BankBalances.Verify(x => x.Update(inserted));
        }
    }
}