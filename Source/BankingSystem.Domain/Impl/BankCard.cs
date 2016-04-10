using System;

namespace BankingSystem.Domain.Impl
{
    /// <summary>
    ///     Represents a bank card.
    /// </summary>
    /// <seealso cref="IBankCard" />
    public class BankCard : IBankCard
    {
        protected BankCard()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BankCard" /> class.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="csv">The CSV.</param>
        /// <param name="pin">The pin.</param>
        /// <param name="cardHolder">The card holder.</param>
        /// <param name="cardNumber">The card number.</param>
        /// <param name="expireMonth">The expire month.</param>
        /// <param name="expireYear">The expire year.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public BankCard(string csv, string pin, string cardHolder, string cardNumber, int expireMonth, int expireYear)
        {
            if (string.IsNullOrEmpty(csv)) throw new ArgumentNullException(nameof(csv));
            if (string.IsNullOrEmpty(pin)) throw new ArgumentNullException(nameof(pin));
            if (string.IsNullOrEmpty(cardHolder)) throw new ArgumentNullException(nameof(cardHolder));
            if (string.IsNullOrEmpty(cardNumber)) throw new ArgumentNullException(nameof(cardNumber));

            CsvCode = csv;
            PinCode = pin;
            CardHolder = cardHolder;
            CardNumber = cardNumber;
            ExpirationMonth = expireMonth;
            ExpirationYear = expireYear;
        }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public virtual int Id { get; protected set; }

        /// <summary>
        ///     Gets the account.
        /// </summary>
        /// <value>
        ///     The account.
        /// </value>
        public virtual IAccount Account { get; protected internal set; }

        /// <summary>
        ///     Gets the CSV security code.
        /// </summary>
        /// <value>
        ///     The CSV security code.
        /// </value>
        public virtual string CsvCode { get; protected set; }

        /// <summary>
        ///     Gets or sets the pin code.
        /// </summary>
        /// <value>
        ///     The pin code.
        /// </value>
        public virtual string PinCode { get; set; }

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