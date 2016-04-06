namespace BankingSystem.Data
{
    /// <summary>
    ///     Defines an account.
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        int Id { get; }

        /// <summary>
        ///     Gets the account number.
        /// </summary>
        /// <value>
        ///     The account number.
        /// </value>
        string AccountNumber { get; }

        /// <summary>
        ///     Gets the currency.
        /// </summary>
        /// <value>
        ///     The currency.
        /// </value>
        string Currency { get; }

        /// <summary>
        ///     Gets the balance.
        /// </summary>
        /// <value>
        ///     The balance.
        /// </value>
        decimal Balance { get; set; }

        /// <summary>
        ///     Gets the assigned bank card.
        /// </summary>
        /// <value>
        ///     The bank card.
        /// </value>
        IBankCard BankCard { get; }
    }
}