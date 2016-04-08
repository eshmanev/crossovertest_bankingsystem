﻿using System;
using System.Collections.Generic;
using BankingSystem.Common.Data;
using BankingSystem.DataTier;
using BankingSystem.DataTier.Entities;

namespace BankingSystem.LogicTier.Impl
{
    /// <summary>
    ///     Represents a service of journals.
    /// </summary>
    /// <seealso cref="BankingSystem.LogicTier.IJournalService" />
    public class JournalService : IJournalService
    {
        private readonly IDatabaseContext _databaseContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="JournalService" /> class.
        /// </summary>
        /// <param name="databaseContext">The database context.</param>
        public JournalService(IDatabaseContext databaseContext)
        {
            if (databaseContext == null)
                throw new ArgumentNullException(nameof(databaseContext));
            _databaseContext = databaseContext;
        }

        /// <summary>
        ///     Gets the customer journals.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public IList<IJournal> GetCustomerJournals(int customerId)
        {
            return _databaseContext.Journals.GetCustomerJournals(customerId);
        }

        /// <summary>
        ///     Writes the journal of transfer between two accounts.
        /// </summary>
        /// <param name="sourceAccount">The source account.</param>
        /// <param name="destAccount">The dest account.</param>
        /// <param name="description">The description.</param>
        /// <param name="commission">The commission.</param>
        /// <returns></returns>
        public IJournal[] WriteTransferJournal(IAccount sourceAccount, IAccount destAccount, string description, decimal commission)
        {
            // write journals
            var sourceCustomer = _databaseContext.Customers.FindByAccount(sourceAccount.AccountNumber);
            var destCustomer = _databaseContext.Customers.FindByAccount(destAccount.AccountNumber);

            // in case of commission, source description should include it
            var sourceDescription = commission > 0 ? $"{description} (Bank commission {commission.ToString("N2")} {sourceAccount.Currency})" : description;

            var journals = new List<IJournal>();
            if (sourceCustomer == destCustomer)
            {
                journals.Add(WriteJournal(sourceCustomer, sourceDescription));
            }
            else
            {
                journals.Add(WriteJournal(sourceCustomer, sourceDescription));
                journals.Add(WriteJournal(destCustomer, description));
            }
            return journals.ToArray();
        }

        /// <summary>
        ///     Writes the journal.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public IJournal WriteJournal(IAccount account, string description)
        {
            var customer = _databaseContext.Customers.FindByAccount(account.AccountNumber);
            return WriteJournal(customer, description);
        }

        /// <summary>
        ///     Writes the journal.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public IJournal WriteJournal(ICustomer customer, string description)
        {
            var journal = new Journal(DateTime.UtcNow, customer, description);
            _databaseContext.Journals.Insert(journal);
            return journal;
        }
    }
}