namespace BankingSystem.Common.Messages
{
    /// <summary>
    ///     Represents a message sent by a client to get current balance.
    /// </summary>
    public class GetBalanceMessage
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
    }
}