using System;
using System.Linq;
using BankingSystem.Domain;
using BankingSystem.LogicTier.Impl;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace BankingSystem.UnitTests.Services
{
    [TestFixture]
    public class JournalServiceTests
    {
        private FakeDatabaseContext _context;
        private JournalService _service;

        [SetUp]
        public void Setup()
        {
            _context = new FakeDatabaseContext();
            _service = new JournalService(_context);
        }

        [Test]
        public void WriteTransferJournal_SameCustomer()
        {
            // arrange
            var customer = Mock.Of<ICustomer>();
            _context.Customers.Setup(x => x.FindByAccount(It.IsAny<string>())).Returns(customer);

            var sourceAccount = new Mock<IAccount>();
            sourceAccount.SetupGet(x => x.Currency).Returns("USD");
            var destAccount = new Mock<IAccount>();
            destAccount.SetupGet(x => x.Currency).Returns("EUR");

            // act
            var journals = _service.WriteTransferJournal(sourceAccount.Object, destAccount.Object, "source description", "dest description").ToList();

            // assert
            journals.Count.ShouldBe(1);
            journals[0].DateTimeCreated.ShouldBeGreaterThan(DateTime.UtcNow.AddMinutes(-5));
            journals[0].Description.ShouldBe("source description");
            journals[0].Customer.ShouldBe(customer);
        }

        [Test]
        public void WriteTransferJournal_DifferentCustomers()
        {
            // arrange
            var customer = Mock.Of<ICustomer>();
            _context.Customers.Setup(x => x.FindByAccount(It.IsAny<string>())).Returns(customer);

            var sourceAccount = new Mock<IAccount>();
            sourceAccount.SetupGet(x => x.Currency).Returns("USD");
            sourceAccount.SetupGet(x => x.AccountNumber).Returns("1111111111");

            var destAccount = new Mock<IAccount>();
            destAccount.SetupGet(x => x.Currency).Returns("EUR");
            destAccount.SetupGet(x => x.AccountNumber).Returns("22222222222");

            var sourceCustomer = Mock.Of<ICustomer>();
            var destCustomer = Mock.Of<ICustomer>();
            _context.Customers.Setup(x => x.FindByAccount(sourceAccount.Object.AccountNumber)).Returns(sourceCustomer);
            _context.Customers.Setup(x => x.FindByAccount(destAccount.Object.AccountNumber)).Returns(destCustomer);

            // act
            var journals = _service.WriteTransferJournal(sourceAccount.Object, destAccount.Object, "source description", "dest description").ToList();

            // assert
            journals.Count.ShouldBe(2);
            journals[0].DateTimeCreated.ShouldBeGreaterThan(DateTime.UtcNow.AddMinutes(-5));
            journals[0].Description.ShouldBe("source description");
            journals[0].Customer.ShouldBe(sourceCustomer);

            journals[1].DateTimeCreated.ShouldBeGreaterThan(DateTime.UtcNow.AddMinutes(-5));
            journals[1].Description.ShouldBe("dest description");
            journals[1].Customer.ShouldBe(destCustomer);
        }

        [Test]
        public void ShouldUpdateBalance_Success()
        {
            // arrange
            var sourceAccount = new Mock<IAccount>();
            sourceAccount.SetupGet(x => x.Balance).Returns(1000m);

            // act
            var journal = _service.WriteJournal(sourceAccount.Object, "description");

            // assert
            journal.DateTimeCreated.ShouldBeGreaterThan(DateTime.UtcNow.AddMinutes(-5));
            journal.Description.ShouldBe("description");
        }
    }
}