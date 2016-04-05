using System;
using System.Collections.Generic;
using BankingSystem.Data;
using BankingSystem.DAL.Entities;
using NHibernate;

namespace BankingSystem.DAL.Session
{
    /// <summary>
    /// Represents a database context.
    /// </summary>
    /// <seealso cref="BankingSystem.DAL.IDatabaseContext" />
    public class DatabaseContext : IDatabaseContext
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private readonly ISessionFactoryHolder _sessionFactoryHolder;
        private ISession _session;
        private ITransaction _transaction;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatabaseContext" /> class.
        /// </summary>
        /// <param name="sessionFactoryHolder">The session factory holder.</param>
        public DatabaseContext(ISessionFactoryHolder sessionFactoryHolder)
        {
            _sessionFactoryHolder = sessionFactoryHolder;
        }

        private bool IsInTransaction => _transaction != null && _transaction.IsActive;

        /// <summary>
        ///     Gets the repository of customers.
        /// </summary>
        /// <value>
        ///     The repository of customers.
        /// </value>
        public IRepository<ICustomer> Customers => GetRepository<ICustomer>();

        /// <summary>
        ///     Gets the repository of login information.
        /// </summary>
        /// <value>
        ///     The repository of login information.
        /// </value>
        public IRepository<ILoginInfo> LoginInfos => GetRepository<ILoginInfo>();

        /// <summary>
        ///     Demands the transaction.
        /// </summary>
        public void DemandTransaction()
        {
            if (_transaction == null)
            {
                _transaction = GetSession().BeginTransaction();
            }
        }

        /// <summary>
        ///     Gets the session.
        /// </summary>
        /// <returns></returns>
        public ISession GetSession()
        {
            return _session ?? (_session = _sessionFactoryHolder.SessionFactory.OpenSession());
        }

        /// <summary>
        ///     Commits this instance.
        /// </summary>
        public void Commit()
        {
            if (!IsInTransaction)
            {
                return;
            }

            _session.Transaction.Commit();
        }

        /// <summary>
        ///     Rollbacks this instance.
        /// </summary>
        public void Rollback()
        {
            if (!IsInTransaction)
            {
                return;
            }

            _session.Transaction.Rollback();
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            DisposeTransaction();
            DisposeSession();
        }

        private void DisposeSession()
        {
            _session?.Dispose();
        }

        private void DisposeTransaction()
        {
            if (!IsInTransaction)
            {
                return;
            }

            _session.Transaction.Dispose();
        }

        private IRepository<T> GetRepository<T>()
            where T : class
        {
            var type = typeof (T);
            object repository;
            if (!_repositories.TryGetValue(type, out repository))
            {
                repository = new Repository<T>(this);
                _repositories.Add(type, repository);
            }
            return (IRepository<T>) repository;
        }
    }
}