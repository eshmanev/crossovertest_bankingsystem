namespace BankingSystem.Common.Messages
{
    /// <summary>
    ///     Provides information about new journal entry.
    /// </summary>
    public class JournalCreatedMessage
    {
        /// <summary>
        ///     Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        ///     The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        ///     Gets or sets the date created.
        /// </summary>
        /// <value>
        ///     The date created.
        /// </value>
        public string DateCreated { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public string Description { get; set; }
    }
}