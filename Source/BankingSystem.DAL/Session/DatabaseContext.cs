using System;
using System.Collections.Generic;
using System.Data;
using BankingSystem.Data;
using NHibernate;
using NHibernate.Transaction;

namespace BankingSystem.DAL.Session
{
    /// <summary>
    ///     Represents a database context.
    /// </summary>
    /// <seealso cref="BankingSystem.DAL.IDatabaseContext" />
    public class DatabaseContext : IDatabaseContext
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private readonly ISessionFactoryHolder _sessionFactoryHolder;
        private readonly Stack<ITransaction> _transactions = new Stack<ITransaction>();
        private ISession _session;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatabaseContext" /> class.
        /// </summary>
        /// <param name="sessionFactoryHolder">The session factory holder.</param>
        public DatabaseContext(ISessionFactoryHolder sessionFactoryHolder)
        {
            _sessionFactoryHolder = sessionFactoryHolder;
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is in transaction.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is in transaction; otherwise, <c>false</c>.
        /// </value>
        private bool IsInTransaction => _transactions.Count > 0;

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
        ///     Gets the repository of operations.
        /// </summary>
        /// <value>
        ///     The repository of operations.
        /// </value>
        public IRepository<IOperation> Operations => GetRepository<IOperation>();

        /// <summary>
        ///     Gets the repository of accounts.
        /// </summary>
        /// <value>
        ///     The repository of accounts.
        /// </value>
        public IRepository<IAccount> Accounts => GetRepository<IAccount>();

        /// <summary>
        ///     Gets the repository of bank balances.
        /// </summary>
        /// <value>
        ///     The repository of bank balances.
        /// </value>
        public IRepository<IBankBalance> BankBalances => GetRepository<IBankBalance>();

        /// <summary>
        ///     Gets the session.
        /// </summary>
        /// <returns></returns>
        public ISession GetSession()
        {
            return _session ?? (_session = _sessionFactoryHolder.SessionFactory.OpenSession());
        }

        /// <summary>
        ///     Demands the transaction scope.
        /// </summary>
        public void DemandTransactionScope()
        {
            var transaction = GetCurrentTransaction();
            transaction = transaction == null ? GetSession().BeginTransaction() : new TransactionWrapper(transaction);
            _transactions.Push(transaction);
        }

        /// <summary>
        /// Commits the transaction scope if it was started.
        /// </summary>
        public void CommitTransactionScope()
        {
            if (!IsInTransaction)
            {
                return;
            }

            GetCurrentTransaction().Commit();
            DisposeTransaction();
        }

        /// <summary>
        /// Rollbacks the transaction scope if it was started.
        /// </summary>
        public void RollbackTransactionScope()
        {
            if (!IsInTransaction)
            {
                return;
            }

            GetCurrentTransaction().Rollback();
            DisposeTransaction();
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

            _transactions.Pop().Dispose();
        }

        private ITransaction GetCurrentTransaction()
        {
            return _transactions.Count > 0 ? _transactions.Peek() : null;
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

        private class TransactionWrapper : ITransaction
        {
            private readonly ITransaction _parentScope;

            public TransactionWrapper(ITransaction parentScope)
            {
                _parentScope = parentScope;
            }

            public void Dispose()
            {
            }

            public void Begin()
            {
            }

            public void Begin(IsolationLevel isolationLevel)
            {
            }

            public void Commit()
            {
                WasCommitted = true;
            }

            public void Rollback()
            {
                WasRolledBack = true;
                _parentScope.Rollback();
            }

            public void Enlist(IDbCommand command)
            {
            }

            public void RegisterSynchronization(ISynchronization synchronization)
            {
            }

            public bool IsActive => true;
            public bool WasRolledBack { get; private set; }
            public bool WasCommitted { get; private set; }
        }
    }
}