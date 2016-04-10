using System.Collections.Generic;
using System.Linq;
using BankingSystem.Domain;

namespace BankingSystem.DataTier.Repositories.Impl
{
    /// <summary>
    ///     Represents a repository of scheduled emails.
    /// </summary>
    public class ScheduledEmailRepository : Repository<IScheduledEmail>, IScheduledEmailRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ScheduledEmailRepository" /> class.
        /// </summary>
        /// <param name="databaseContext"></param>
        public ScheduledEmailRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        /// <summary>
        ///     Searches all emails for the specified recipient address.
        /// </summary>
        /// <param name="recipientAddress">The recipient address.</param>
        /// <returns>
        ///     A collection of the emails
        /// </returns>
        public IEnumerable<IScheduledEmail> FindByRecipientAddress(string recipientAddress)
        {
            return Filter(x => x.RecipientAddress == recipientAddress).ToList();
        }
    }
}