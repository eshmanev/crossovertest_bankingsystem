using System;
using BankingSystem.DataTier.Repositories;
using NHibernate;

namespace BankingSystem.DataTier
{
    /// <summary>
    ///     Holds the NHibernate session.
    /// </summary>
    public interface IDatabaseContext : IDisposable
    {
        /// <summary>
        ///     Gets the repository of customers.
        /// </summary>
        /// <value>
        ///     The repository of customers.
        /// </value>
        ICustomerRepository Customers { get; }

        /// <summary>
        ///     Gets the repository of login information.
        /// </summary>
        /// <value>
        ///     The repository of login information.
        /// </value>
        ILoginInfoRepository LoginInfos { get; }

        /// <summary>
        ///     Gets the repository of operations.
        /// </summary>
        /// <value>
        ///     The repository of operations.
        /// </value>
        IOperationRepository Operations { get; }

        /// <summary>
        ///     Gets the repository of accounts.
        /// </summary>
        /// <value>
        ///     The repository of accounts.
        /// </value>
        IAccountRepository Accounts { get; }

        /// <summary>
        ///     Gets the repository of bank balances.
        /// </summary>
        /// <value>
        ///     The repository of bank balances.
        /// </value>
        IBankBalanceRepository BankBalances { get; }

        /// <summary>
        ///     Gets the repository of scheduled emails.
        /// </summary>
        /// <value>
        ///     The repository of scheduled emails.
        /// </value>
        IScheduledEmailRepository ScheduledEmails { get; }

        /// <summary>
        ///     Gets the repository of delivered emails.
        /// </summary>
        /// <value>
        ///     The repository of delivered emails.
        /// </value>
        IDeliveredEmailRepository DeliveredEmails { get; }

        /// <summary>
        ///     Gets the current session.
        /// </summary>
        /// <returns>The session</returns>
        ISession GetSession();

        /// <summary>
        ///     Demands the transaction.
        /// </summary>
        IDatabaseTransaction DemandTransaction();
    }
}