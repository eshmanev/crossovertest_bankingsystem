using BankingSystem.Data;

namespace BankingSystem.DAL.Entities
{
    /// <summary>
    ///     Represents a bank card.
    /// </summary>
    /// <seealso cref="BankingSystem.Data.IBankCard" />
    public class BankCard : IBankCard
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public virtual int Id { get; protected set; }

        /// <summary>
        ///     Gets the CSV security code.
        /// </summary>
        /// <value>
        ///     The CSV security code.
        /// </value>
        public virtual string CsvCode { get; protected set; }

        /// <summary>
        ///     Gets the name of the card holder.
        /// </summary>
        /// <value>
        ///     The name of the card holder.
        /// </value>
        public virtual string CardHolder { get; protected set; }

        /// <summary>
        ///     Gets the card number.
        /// </summary>
        /// <value>
        ///     The card number.
        /// </value>
        public virtual string CardNumber { get; protected set; }

        /// <summary>
        ///     Gets the expiration month.
        /// </summary>
        /// <value>
        ///     The expiration month.
        /// </value>
        public virtual int ExpirationMonth { get; protected set; }

        /// <summary>
        ///     Gets the expiration year.
        /// </summary>
        /// <value>
        ///     The expiration year.
        /// </value>
        public virtual int ExpirationYear { get; protected set; }
    }
}