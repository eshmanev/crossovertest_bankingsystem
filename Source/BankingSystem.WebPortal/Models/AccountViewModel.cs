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
    }
}