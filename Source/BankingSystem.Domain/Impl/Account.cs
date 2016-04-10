using System;

namespace BankingSystem.Domain.Impl
{
    /// <summary>
    ///     Represents an account.
    /// </summary>
    /// <seealso cref="IAccount" />
    internal class Account : IAccount
    {
        // <summary>
        //     These field exists to support NH persistence.
        //     On a save, these are the ones that are actually persisted in the NH map.
        // </summary>
        // ReSharper disable once NotAccessedField.Local
        private readonly CustomerBase _customer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Account" /> class.
        /// </summary>
        protected Account()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Account" /> class.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="customer">The customer.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        internal Account(string accountNumber, string currency, CustomerBase customer)
        {
            _customer = customer;
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentNullException(nameof(accountNumber));

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentNullException(nameof(currency));

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            AccountNumber = accountNumber;
            Currency = currency;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public virtual int Id { get; protected set; }

        /// <summary>
        ///     Gets the account number.
        /// </summary>
        /// <value>
        ///     The account number.
        /// </value>
        public virtual string AccountNumber { get; protected set; }

        /// <summary>
        ///     Gets the currency.
        /// </summary>
        /// <value>
        ///     The currency.
        /// </value>
        public virtual string Currency { get; protected set; }

        /// <summary>
        ///     Gets the balance.
        /// </summary>
        /// <value>
        ///     The balance.
        /// </value>
        public virtual decimal Balance { get; set; }

        /// <summary>
        ///     Gets the assigned bank card.
        /// </summary>
        /// <value>
        ///     The bank card.
        /// </value>
        public virtual IBankCard BankCard { get; protected set; }


        /// <summary>
        ///     This stupid workaround is to support NH persistence.
        ///     On a save, these are the ones that are actually persisted in the NH map.
        /// </summary>
        private int _customerId;
        protected virtual int CustomerId { get { return _customer?.Id ?? _customerId; } set { _customerId = value; } }
    }
}