namespace BankingSystem.Common.Data
{
    /// <summary>
    ///     Defines a bank card.
    /// </summary>
    public interface IBankCard
    {
        /// <summary>
        ///     Gets the account.
        /// </summary>
        /// <value>
        ///     The account.
        /// </value>
        IAccount Account { get; }

        /// <summary>
        ///     Gets the CSV security code.
        /// </summary>
        /// <value>
        ///     The CSV security code.
        /// </value>
        string CsvCode { get; }

        /// <summary>
        ///     Gets or sets the pin code.
        /// </summary>
        /// <value>
        ///     The pin code.
        /// </value>
        string PinCode { get; set; }

        /// <summary>
        ///     Gets the name of the card holder.
        /// </summary>
        /// <value>
        ///     The name of the card holder.
        /// </value>
        string CardHolder { get; }

        /// <summary>
        ///     Gets the card number.
        /// </summary>
        /// <value>
        ///     The card number.
        /// </value>
        string CardNumber { get; }

        /// <summary>
        ///     Gets the expiration month.
        /// </summary>
        /// <value>
        ///     The expiration month.
        /// </value>
        int ExpirationMonth { get; }

        /// <summary>
        ///     Gets the expiration year.
        /// </summary>
        /// <value>
        ///     The expiration year.
        /// </value>
        int ExpirationYear { get; }
    }
}