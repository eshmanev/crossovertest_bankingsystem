﻿using System;
using System.Collections.Generic;
using BankingSystem.Data;
using NHibernate;

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
        private readonly Stack<IDatabaseTransaction> _transactions = new Stack<IDatabaseTransaction>();
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
        ///     Demands the transaction.
        /// </summary>
        public IDatabaseTransaction DemandTransaction()
        {
            var transaction = GetCurrentTransaction();
            return transaction == null 
                ? new DatabaseTransaction(this, GetSession().BeginTransaction())
                : new TransactionScope(this, transaction);
        }

        /// <summary>
        /// Commits the transaction if it was started.
        /// </summary>
        public void CommitTransaction()
        {
            if (!IsInTransaction)
            {
                return;
            }

            GetCurrentTransaction().Commit();
            DisposeTransaction();
        }

        /// <summary>
        /// Rollbacks the transaction if it was started.
        /// </summary>
        public void RollbackTransaction()
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

        private IDatabaseTransaction GetCurrentTransaction()
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

        private class TransactionScope : IDatabaseTransaction
        {
            private readonly DatabaseContext _context;
            private readonly IDatabaseTransaction _parentScope;
            private bool _wasRolledBack;
            private bool _wasCommitted;
            private bool _isDisposed;

            public TransactionScope(DatabaseContext context, IDatabaseTransaction parentScope)
            {
                _context = context;
                _parentScope = parentScope;
                _context._transactions.Push(this);
            }

            public void Dispose()
            {
                if (!_isDisposed)
                {
                    DisposeCore();
                    _isDisposed = true;
                }
            }

            public void Commit()
            {
                if (!_wasRolledBack && !_wasCommitted)
                {
                    CommitCore();
                    _wasCommitted = true;
                }
            }

            public void Rollback()
            {
                if (!_wasRolledBack && !_wasCommitted)
                {
                    RollbackCore();
                    _wasRolledBack = true;
                }
            }

            protected virtual void DisposeCore()
            {
                if (!_wasRolledBack && !_wasCommitted)
                    Rollback();

                _context._transactions.Pop();
            }

            protected virtual void CommitCore()
            {
            }

            protected virtual void RollbackCore()
            {
                _parentScope?.Rollback();
            }
        }

        private class DatabaseTransaction : TransactionScope
        {
            private readonly ITransaction _transaction;

            public DatabaseTransaction(DatabaseContext context, ITransaction transaction) 
                : base(context, null)
            {
                _transaction = transaction;
            }

            protected override void DisposeCore()
            {
                base.DisposeCore();
                _transaction.Dispose();
            }

            protected override void CommitCore()
            {
                base.CommitCore();
                _transaction.Commit();
            }

            protected override void RollbackCore()
            {
                base.RollbackCore();
                _transaction.Rollback();
            }
        }
    }
}