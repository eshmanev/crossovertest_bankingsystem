namespace BankingSystem.WebPortal.Models
{
    /// <summary>
    ///     Represents an account view model.
    /// </summary>
    public class AccountViewModel
    {
        /// <summary>
        ///     Gets the account number.
        /// </summary>
        /// <value>
        ///     The account number.
        /// </value>
        public virtual string AccountNumber { get; set; }

        /// <summary>
        ///     Gets the currency.
        /// </summary>
        /// <value>
        ///     The currency.
        /// </value>
        public virtual string Currency { get; set; }

        /// <summary>
        ///     Gets the balance.
        /// </summary>
        /// <value>
        ///     The balance.
        /// </value>
        public virtual decimal Balance { get; set; }

        /// <summary>
        ///     Gets or sets the card number.
        /// </summary>
        /// <value>
        ///     The card number.
        /// </value>
        public string CardNumber { get; set; }

        /// <summary>
        ///     Gets or sets the card expiration.
        /// </summary>
        /// <value>
        ///     The card expiration.
        /// </value>
        public string CardExpiration { get; set; }

        /// <summary>
        ///     Gets or sets the card holder.
        /// </summary>
        /// <value>
        ///     The card holder.
        /// </value>
        public string CardHolder { get; set; }
    }
}