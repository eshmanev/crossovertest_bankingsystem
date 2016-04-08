using System;
using BankingSystem.DataTier;
using BankingSystem.DataTier.Repositories;
using Moq;
using NHibernate;

namespace BankingSystem.UnitTests.Services
{
    public class FakeDatabaseContext : IDatabaseContext
    {
        public FakeDatabaseContext()
        {
            Customers = new Mock<ICustomerRepository>();
            LoginInfos = new Mock<ILoginInfoRepository>();
            Operations = new Mock<IJournalRepository>();
            Accounts = new Mock<IAccountRepository>();
            BankBalances = new Mock<IBankBalanceRepository>();
            ScheduledEmails = new Mock<IScheduledEmailRepository>();
            DeliveredEmails = new Mock<IDeliveredEmailRepository>();
            BankCards = new Mock<IBankCardRepository>();
            Session = new Mock<ISession>();
            Transaction = new Mock<IDatabaseTransaction>();
        }

        public Mock<ICustomerRepository> Customers { get; }
        public Mock<ILoginInfoRepository> LoginInfos { get; }
        public Mock<IJournalRepository> Operations { get; }
        public Mock<IAccountRepository> Accounts { get; }
        public Mock<IBankBalanceRepository> BankBalances { get; }
        public Mock<IScheduledEmailRepository> ScheduledEmails { get; }
        public Mock<IDeliveredEmailRepository> DeliveredEmails { get; }
        public Mock<IBankCardRepository> BankCards { get; }
        public Mock<ISession> Session { get; }
        public Mock<IDatabaseTransaction> Transaction { get; }

        ICustomerRepository IDatabaseContext.Customers => Customers.Object;
        ILoginInfoRepository IDatabaseContext.LoginInfos => LoginInfos.Object;
        IJournalRepository IDatabaseContext.Journals => Operations.Object;
        IAccountRepository IDatabaseContext.Accounts => Accounts.Object;
        IBankBalanceRepository IDatabaseContext.BankBalances => BankBalances.Object;
        IScheduledEmailRepository IDatabaseContext.ScheduledEmails => ScheduledEmails.Object;
        IDeliveredEmailRepository IDatabaseContext.DeliveredEmails => DeliveredEmails.Object;
        IBankCardRepository IDatabaseContext.BankCards => BankCards.Object;

        ISession IDatabaseContext.GetSession()
        {
            return Session.Object;
        }

        IDatabaseTransaction IDatabaseContext.DemandTransaction()
        {
            return Transaction.Object;
        }

        void IDisposable.Dispose()
        {
        }
    }
}