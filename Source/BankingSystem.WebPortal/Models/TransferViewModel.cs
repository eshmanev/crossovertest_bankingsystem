namespace BankingSystem.WebPortal.Models
{
    /// <summary>
    ///     Represents a transfer view model.
    /// </summary>
    public class TransferViewModel
    {
        /// <summary>
        ///     Gets or sets the source account.
        /// </summary>
        /// <value>
        ///     The source account.
        /// </value>
        public string SourceAccount { get; set; }

        /// <summary>
        ///     Gets or sets the dest account.
        /// </summary>
        /// <value>
        ///     The dest account.
        /// </value>
        public string DestAccount { get; set; }

        /// <summary>
        ///     Gets or sets the amount.
        /// </summary>
        /// <value>
        ///     The amount.
        /// </value>
        public decimal Amount { get; set; }
    }
}