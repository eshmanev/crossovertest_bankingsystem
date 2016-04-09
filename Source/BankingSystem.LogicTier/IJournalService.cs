using System.Collections.Generic;
using BankingSystem.Domain;

namespace BankingSystem.LogicTier
{
    /// <summary>
    ///     Defines a service of journals.
    /// </summary>
    public interface IJournalService
    {
        /// <summary>
        ///     Gets the customer journals.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        IList<IJournal> GetCustomerJournals(int customerId);

        /// <summary>
        ///     Writes the journal of transfer between two accounts.
        /// </summary>
        /// <param name="sourceAccount">The source account.</param>
        /// <param name="destAccount">The dest account.</param>
        /// <param name="sourceDescription">The source description.</param>
        /// <param name="destDescription">The dest description</param>
        /// <returns>An array of created journals.</returns>
        IJournal[] WriteTransferJournal(IAccount sourceAccount, IAccount destAccount, string sourceDescription, string destDescription);

        /// <summary>
        ///     Writes the journal.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="description">The description.</param>
        /// <returns>The created journal.</returns>
        IJournal WriteJournal(IAccount account, string description);

        /// <summary>
        ///     Writes the journal.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="description">The description.</param>
        /// <returns>The created journal.</returns>
        IJournal WriteJournal(ICustomer customer, string description);
    }
}