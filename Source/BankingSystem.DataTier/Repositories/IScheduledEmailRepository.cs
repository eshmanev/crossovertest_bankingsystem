using System.Collections.Generic;
using BankingSystem.Domain;

namespace BankingSystem.DataTier.Repositories
{
    /// <summary>
    ///     Defines a repository of scheduled emails.
    /// </summary>
    public interface IScheduledEmailRepository : IRepository<IScheduledEmail>
    {
        /// <summary>
        ///     Searches all emails for the specified recipient address.
        /// </summary>
        /// <param name="recipientAddress">The recipient address.</param>
        /// <returns>A collection of the emails</returns>
        IEnumerable<IScheduledEmail> FindByRecipientAddress(string recipientAddress);
    }
}