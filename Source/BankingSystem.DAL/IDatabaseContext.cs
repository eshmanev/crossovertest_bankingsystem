using System;
using BankingSystem.Data;
using BankingSystem.DAL.Entities;
using NHibernate;

namespace BankingSystem.DAL
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
        ///     Demands the transaction.
        /// </summary>
        void DemandTransaction();

        /// <summary>
        ///     Gets the current session.
        /// </summary>
        /// <returns>The session</returns>
        ISession GetSession();

        /// <summary>
        ///     Commits the transaction if it was started.
        /// </summary>
        void Commit();

        /// <summary>
        ///     Rollbacks the transaction if it was started.
        /// </summary>
        void Rollback();
    }
}