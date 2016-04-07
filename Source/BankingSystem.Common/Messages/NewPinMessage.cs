namespace BankingSystem.Common.Messages
{
    /// <summary>
    ///     Represents a message sent by clients to change a bank card's PIN code.
    /// </summary>
    public class NewPinMessage
    {
        /// <summary>
        ///     Gets or sets the new pin.
        /// </summary>
        /// <value>
        ///     The new pin.
        /// </value>
        public string NewPin { get; set; }
    }
}