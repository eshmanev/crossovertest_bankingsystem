namespace BankingSystem.Common.Messages
{
    /// <summary>
    ///     Represents a message sent by a client to withdraw cache from the specified bank card.
    /// </summary>
    public class WithdrawMessage
    {
        /// <summary>
        ///     Gets or sets the bank number.
        /// </summary>
        /// <value>
        ///     The bank number.
        /// </value>
        public string BankNumber { get; set; }

        /// <summary>
        ///     Gets or sets the pin code.
        /// </summary>
        /// <value>
        ///     The pin code.
        /// </value>
        public string PinCode { get; set; }

        /// <summary>
        ///     Gets or sets the amount to withdraw.
        /// </summary>
        /// <value>
        ///     The amount.
        /// </value>
        public decimal Amount { get; set; }
    }
}