namespace BankingSystem.Common.Messages
{
    /// <summary>
    ///     Represents a message sent by a client to change the specified bank card's PIN code.
    /// </summary>
    public class ChangePinMessage
    {
        /// <summary>
        ///     Gets or sets the bank number.
        /// </summary>
        /// <value>
        ///     The bank number.
        /// </value>
        public string BankNumber { get; set; }

        /// <summary>
        ///     Gets or sets the old pin code.
        /// </summary>
        /// <value>
        ///     The old pin code.
        /// </value>
        public string OldPinCode { get; set; }

        /// <summary>
        ///     Gets or sets the new pin code.
        /// </summary>
        /// <value>
        ///     The new pin code.
        /// </value>
        public string NewPinCode { get; set; }
    }
}