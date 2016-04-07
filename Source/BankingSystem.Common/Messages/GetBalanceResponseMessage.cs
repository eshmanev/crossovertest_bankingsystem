namespace BankingSystem.Common.Messages
{
    /// <summary>
    ///     Represents a message sent to a client as a response to <see cref="GetBalanceMessage" />.
    /// </summary>
    public class GetBalanceResponseMessage
    {
        /// <summary>
        ///     Gets or sets the amount.
        /// </summary>
        /// <value>
        ///     The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        ///     Gets or sets the currency.
        /// </summary>
        /// <value>
        ///     The currency.
        /// </value>
        public string Currency { get; set; }
    }
}