namespace BankingSystem.Common.Messages
{
    /// <summary>
    ///     Provides information about changes in account balance.
    /// </summary>
    public class BalanceChangedMessage
    {
        /// <summary>
        ///     Gets or sets the account number.
        /// </summary>
        /// <value>
        ///     The account number.
        /// </value>
        public string AccountNumber { get; set; }

        /// <summary>
        ///     Gets or sets the amount.
        ///     Positive value means that the amount is increased; negative value means that the amount is decreased.
        /// </summary>
        /// <value>
        ///     The amount.
        /// </value>
        public decimal ChangeAmount { get; set; }

        /// <summary>
        ///     Gets or sets the current balance.
        /// </summary>
        /// <value>
        ///     The current balance.
        /// </value>
        public decimal CurrentBalance { get; set; }

        /// <summary>
        ///     Gets or sets the currency.
        /// </summary>
        /// <value>
        ///     The currency.
        /// </value>
        public string Currency { get; set; }
    }
}