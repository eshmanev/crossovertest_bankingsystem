using System;
using System.Collections.Generic;
using BankingSystem.Common.Data;
using BankingSystem.Common.Messages;
using BankingSystem.LogicTier;
using Microsoft.AspNet.SignalR.Hubs;
using WebGrease.Css.Extensions;

namespace BankingSystem.WebPortal.Services
{
    /// <summary>
    ///     Represents a journal service decorated with automatic notifications.
    /// </summary>
    /// <seealso cref="BankingSystem.LogicTier.IJournalService" />
    public class JournalServiceDecorator : IJournalService
    {
        private readonly IJournalService _original;
        private readonly IHubConnectionContext<dynamic> _hubContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="JournalServiceDecorator" /> class.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="hubContext">The hub context.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public JournalServiceDecorator(IJournalService original, IHubConnectionContext<dynamic> hubContext)
        {
            if (original == null)
                throw new ArgumentNullException(nameof(original));
            if (hubContext == null)
                throw new ArgumentNullException(nameof(hubContext));
            _original = original;
            _hubContext = hubContext;
        }

        /// <summary>
        ///     Gets the customer journals.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public IList<IJournal> GetCustomerJournals(int customerId)
        {
            return _original.GetCustomerJournals(customerId);
        }

        /// <summary>
        ///     Writes the journal of transfer between two accounts.
        /// </summary>
        /// <param name="sourceAccount">The source account.</param>
        /// <param name="destAccount">The dest account.</param>
        /// <param name="sourceDescription">The source description.</param>
        /// <param name="destDescription">The dest description</param>
        /// <returns>
        ///     An array of created journals.
        /// </returns>
        public IJournal[] WriteTransferJournal(IAccount sourceAccount, IAccount destAccount, string sourceDescription, string destDescription)
        {
            var journals = _original.WriteTransferJournal(sourceAccount, destAccount, sourceDescription, destDescription);
            journals.ForEach(SendJournal);
            return journals;
        }

        /// <summary>
        ///     Writes the journal.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="description">The description.</param>
        /// <returns>
        ///     The created journal.
        /// </returns>
        public IJournal WriteJournal(IAccount account, string description)
        {
            var journal = _original.WriteJournal(account, description);
            SendJournal(journal);
            return journal;
        }

        /// <summary>
        ///     Writes the journal.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="description">The description.</param>
        /// <returns>
        ///     The created journal.
        /// </returns>
        public IJournal WriteJournal(ICustomer customer, string description)
        {
            var journal = _original.WriteJournal(customer, description);
            SendJournal(journal);
            return journal;
        }

        private void SendJournal(IJournal journal)
        {
            _hubContext.All.onJournalCreated(new JournalCreatedMessage
            {
                CustomerId = journal.Customer.Id,
                DateCreated = journal.DateTimeCreated.ToLocalTime().ToString("MM/dd/yyyy HH:mm"),
                Description = journal.Description
            });
        }
    }
}