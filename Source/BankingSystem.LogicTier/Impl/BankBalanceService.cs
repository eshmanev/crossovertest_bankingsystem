using System;
using System.Linq;
using BankingSystem.DataTier;
using BankingSystem.Domain;
using BankingSystem.Domain.Impl;

namespace BankingSystem.LogicTier.Impl
{
    /// <summary>
    ///     Represents a service of bank balance.
    /// </summary>
    public class BankBalanceService : IBankBalanceService
    {
        private readonly IDatabaseContext _databaseContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BankBalanceService" /> class.
        /// </summary>
        /// <param name="databaseContext">The database context.</param>
        public BankBalanceService(IDatabaseContext databaseContext)
        {
            if (databaseContext == null)
                throw new ArgumentNullException(nameof(databaseContext));

            _databaseContext = databaseContext;
        }

        /// <summary>
        ///     Adds the revenue.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="description">The description.</param>
        public void AddRevenue(decimal amount, string description)
        {
            using (var transaction = _databaseContext.DemandTransaction())
            {
                try
                {
                    var balance = GetOrCreateBalance();
                    balance.TotalBalance += amount;
                    _databaseContext.BankBalances.Update(balance);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        ///     Adds the expences.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="description">The description.</param>
        public void AddExpences(decimal amount, string description)
        {
            using (var transaction = _databaseContext.DemandTransaction())
            {
                try
                {
                    var balance = GetOrCreateBalance();
                    balance.TotalBalance -= amount;
                    _databaseContext.BankBalances.Update(balance);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }  
        }

        private IBankBalance GetOrCreateBalance()
        {
            var balance = _databaseContext.BankBalances.GetAll().FirstOrDefault();
            if (balance == null)
            {
                balance = new BankBalance();
                _databaseContext.BankBalances.Insert(balance);
            }
            return balance;
        }
    }
}