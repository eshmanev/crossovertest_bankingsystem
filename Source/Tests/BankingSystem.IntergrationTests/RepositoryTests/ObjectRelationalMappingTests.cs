using System;
using System.Linq;
using BankingSystem.DataTier;
using BankingSystem.DataTier.Repositories;
using BankingSystem.DataTier.Session;
using BankingSystem.Domain.Impl;
using NUnit.Framework;
using Shouldly;

namespace BankingSystem.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class ObjectRelationalMappingTests
    {
        private IDatabaseContext _databaseContext;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            _databaseContext = new DatabaseContext(new SessionFactoryHolder());
        }

        [TestFixtureTearDown]
        public void FixtureTeardown()
        {
            _databaseContext.Dispose();
        }

        [Test]
        public void TestIndividualWithAccount()
        {
            // arrange
            var individual = new Individual("testuser", "test@test.com", "hash", "Evgeny", "Shamenv");
            var loginInfo = new LoginInfo("testprovider", "testkey");
            individual.AddLogin(loginInfo);

            var account = individual.AddAccount("1234567890", "USD");
            account.BankCard = new BankCard("123", "0000", "EVGENY SHMANEV", "1234567890", 12, 2099);

            // act
            var result = (Individual) RunTest(individual, _databaseContext.Customers, x => x.Id);

            // assert
            result.UserName.ShouldBe(individual.UserName);
            result.Email.ShouldBe(individual.Email);
            result.PasswordHash.ShouldBe(individual.PasswordHash);
            result.FirstName.ShouldBe(individual.FirstName);
            result.LastName.ShouldBe(individual.LastName);

            result.Logins.Count().ShouldBe(1);
            result.Logins.First().LoginKey.ShouldBe(loginInfo.LoginKey);
            result.Logins.First().ProviderName.ShouldBe(loginInfo.ProviderName);

            result.Accounts.Count().ShouldBe(1);
            result.Accounts.First().AccountNumber.ShouldBe(account.AccountNumber);
            result.Accounts.First().Currency.ShouldBe(account.Currency);

            result.Accounts.First().BankCard.CardHolder.ShouldBe(account.BankCard.CardHolder);
            result.Accounts.First().BankCard.CardNumber.ShouldBe(account.BankCard.CardNumber);
            result.Accounts.First().BankCard.CsvCode.ShouldBe(account.BankCard.CsvCode);
            result.Accounts.First().BankCard.ExpirationMonth.ShouldBe(account.BankCard.ExpirationMonth);
            result.Accounts.First().BankCard.ExpirationYear.ShouldBe(account.BankCard.ExpirationYear);
            result.Accounts.First().BankCard.PinCode.ShouldBe(account.BankCard.PinCode);
        }

        [Test]
        public void TestMerchant()
        {
            // arrange
            var merchant = new Merchant("testuser", "test@test.com", "hash", Guid.NewGuid(), "Toy store", "Evgeny Shmanev");

            // act
            var result = (Merchant)RunTest(merchant, _databaseContext.Customers, x => x.Id);

            // assert
            result.UserName.ShouldBe(merchant.UserName);
            result.Email.ShouldBe(merchant.Email);
            result.PasswordHash.ShouldBe(merchant.PasswordHash);
            result.MerchantId.ShouldBe(merchant.MerchantId);
            result.MerchantName.ShouldBe(merchant.MerchantName);
            result.ContactPerson.ShouldBe(merchant.ContactPerson);
        }

        [Test]
        public void TestBankBalance()
        {
            // arrange
            var balance = new BankBalance {TotalBalance = 100};

            // act
            var result = RunTest(balance, _databaseContext.BankBalances, x => ((BankBalance) x).Id);

            // assert
            result.TotalBalance.ShouldBe(balance.TotalBalance);
        }

        [Test]
        public void TestDeliveredEmail()
        {
            // arrange
            var email = new DeliveredEmail {RecipientAddress = "test@test.com", Body = "body", DeliveredDateTime = Truncate(DateTime.UtcNow), Subject = "subject"};

            // act
            var result = RunTest(email, _databaseContext.DeliveredEmails, x => ((DeliveredEmail) x).Id);

            // assert
            result.Body.ShouldBe(email.Body);
            result.RecipientAddress.ShouldBe(email.RecipientAddress);
            result.DeliveredDateTime.ShouldBe(email.DeliveredDateTime);
            result.Subject.ShouldBe(email.Subject);
        }

        [Test]
        public void TestScheduledEmail()
        {
            // arrange
            var email = new ScheduledEmail {RecipientAddress = "test@test.com", Body = "body", ScheduledDateTime = Truncate(DateTime.UtcNow), FailureReason = "failure", Subject = "subject"};

            // act
            var result = RunTest(email, _databaseContext.ScheduledEmails, x => ((ScheduledEmail) x).Id);

            // assert
            result.Body.ShouldBe(email.Body);
            result.RecipientAddress.ShouldBe(email.RecipientAddress);
            result.ScheduledDateTime.ShouldBe(email.ScheduledDateTime);
            result.Subject.ShouldBe(email.Subject);
            result.FailureReason.ShouldBe(email.FailureReason);
        }

        private TEntity RunTest<TEntity>(TEntity newEntity, IRepository<TEntity> repository, Func<TEntity, object> id) where TEntity : class
        {
            TEntity other = null;
            try
            {
                repository.Insert(newEntity);
                _databaseContext.GetSession().Evict(newEntity);
                other = repository.GetById(id(newEntity));
            }
            finally
            {
                repository.Delete(other);
            }
            return other;
        }

        private static DateTime Truncate(DateTime dateTime)
        {
            var timeSpan = TimeSpan.FromSeconds(1);
            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }
    }
}