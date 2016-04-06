using System;
using BankingSystem.Common.Data;
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
        IRepository<ICustomer> Customers { get; }

        /// <summary>
        ///     Gets the repository of login information.
        /// </summary>
        /// <value>
        ///     The repository of login information.
        /// </value>
        IRepository<ILoginInfo> LoginInfos { get; }

        /// <summary>
        ///     Gets the repository of operations.
        /// </summary>
        /// <value>
        ///     The repository of operations.
        /// </value>
        IRepository<IOperation> Operations { get; }

        /// <summary>
        ///     Gets the repository of accounts.
        /// </summary>
        /// <value>
        ///     The repository of accounts.
        /// </value>
        IRepository<IAccount> Accounts { get; }

        /// <summary>
        ///     Gets the repository of bank balances.
        /// </summary>
        /// <value>
        ///     The repository of bank balances.
        /// </value>
        IRepository<IBankBalance> BankBalances { get; }

        /// <summary>
        ///     Gets the repository of scheduled emails.
        /// </summary>
        /// <value>
        ///     The repository of scheduled emails.
        /// </value>
        IRepository<IScheduledEmail> ScheduledEmails { get; }

        /// <summary>
        ///     Gets the repository of delivered emails.
        /// </summary>
        /// <value>
        ///     The repository of delivered emails.
        /// </value>
        IRepository<IDeliveredEmail> DeliveredEmails { get; }

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