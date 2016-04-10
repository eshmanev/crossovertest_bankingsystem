using System.Linq;
using BankingSystem.DataTier;
using BankingSystem.Domain.Impl;
using BankingSystem.IntegrationTests.Environment.Services;
using BankingSystem.LogicTier;
using BankingSystem.Messages;
using BankingSystem.NotificationService.Handlers;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Shouldly;

namespace BankingSystem.IntegrationTests.NotificationsTests
{
    [TestFixture]
    public class EmailNotificationTests : NotificationTestsBase
    {
        [Test]
        public async void ShouldScheduleEmailOnWithdrawal()
        {
            // arrange
            var bankingService = new TestBankingServiceProxy(HttpClient);
            var database = NotificationServer.Container.Resolve<IDatabaseContext>();
            var handler = (SyncBalanceChangedMessageHandler) NotificationServer.Container.Resolve<IHandler<BalanceChangedMessage>>();
            var bankCard = database.BankCards.FindByNumber(TestVars.ValidCardNumber);
            var customer = database.Customers.FindByAccount(bankCard.Account.AccountNumber);

            // act
            var result = await bankingService.Withdraw(TestVars.ValidCardNumber, TestVars.ValidPinCode, 1);

            // assert
            handler.SyncRoot.WaitOne(TestVars.DefaultTimeout);
            result.ShouldBeNull();

            var emails = database.ScheduledEmails.FindByRecipientAddress(customer.Email).ToList();
            emails.ShouldContain(x => x.Subject == "Balance of your account has changed" &&
                                      x.Body.Contains(bankCard.Account.AccountNumber) &&
                                      x.Body.Contains($"Total outcome is 1 {TestVars.ValidCardCurrency}"));

            // cleanup
            bankCard.Account.Balance += 1;
            database.Accounts.Update(bankCard.Account);
        }

        [Test]
        public void ShouldScheduleEmailsOnAccountToAccountTransfer()
        {
            // arrange
            var handler = (SyncBalanceChangedMessageHandler) NotificationServer.Container.Resolve<IHandler<BalanceChangedMessage>>();
            var database = WebApiServer.Container.Resolve<IDatabaseContext>();
            var accountService = WebApiServer.Container.Resolve<IAccountService>();

            var customer = new Individual("testuser123", "testuser123@test.test", "alksdjfas", "Test", "Test");
            var account1 = customer.AddAccount("9999999999999991", "USD");
            var account2 = customer.AddAccount("9999999999999992", "EUR");
            account1.Balance = 100;
            account2.Balance = 0;
            database.Customers.Insert(customer);

            try
            {
                // act
                accountService.TransferMoney(account1, account2, 50, AmountConversionMode.SourceToTarget, "Email Notification Integration Test");
                handler.SyncRoot.WaitOne(TestVars.DefaultTimeout);

                // assert
                account1.Balance.ShouldBe(50);
                account2.Balance.ShouldBeGreaterThan(0);

                var emails = database.ScheduledEmails.FindByRecipientAddress(customer.Email).ToList();

                emails.ShouldContain(x => x.Subject == "Balance of your account has changed" &&
                                          x.Body.Contains(account1.AccountNumber) &&
                                          x.Body.Contains($"Total outcome is 50 USD"));

                emails.ShouldContain(x => x.Subject == "Balance of your account has changed" &&
                                          x.Body.Contains(account2.AccountNumber) &&
                                          x.Body.Contains($"Total income is {account2.Balance} EUR"));
            }
            finally
            {
                database.Customers.Delete(customer);
            }
        }
    }
}