namespace BankingSystem.Common.Messages
{
    /// <summary>
    ///     Represents a message sent by clients to change a bank card's balance.
    /// </summary>
    public class ChangeAmountMessage
    {
        /// <summary>
        ///     Gets or sets the amount which affects the current bank card balance. May be negative or positive.
        /// </summary>
        /// <value>
        ///     The amount.
        /// </value>
        public decimal Amount { get; set; }
    }
}