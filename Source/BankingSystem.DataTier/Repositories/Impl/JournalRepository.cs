using System.Collections.Generic;
using BankingSystem.Domain;

namespace BankingSystem.DataTier.Repositories.Impl
{
    /// <summary>
    ///     Represents a repository of journals.
    /// </summary>
    public class JournalRepository : Repository<IJournal>, IJournalRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JournalRepository" /> class.
        /// </summary>
        /// <param name="databaseContext"></param>
        public JournalRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        /// <summary>
        ///     Gets the customer journals.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns>A list of journals.</returns>
        public IList<IJournal> GetCustomerJournals(int customerId)
        {
            return Filter(x => x.Customer.Id == customerId);
        }
    }
}