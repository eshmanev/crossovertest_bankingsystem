using System.Collections.Generic;
using BankingSystem.Common.Data;

namespace BankingSystem.DataTier.Repositories
{
    /// <summary>
    ///     Defines a repository of journal.
    /// </summary>
    public interface IJournalRepository : IRepository<IJournal>
    {
        /// <summary>
        ///     Gets the customer journals.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns>A list of journals.</returns>
        IList<IJournal> GetCustomerJournals(int customerId);
    }
}