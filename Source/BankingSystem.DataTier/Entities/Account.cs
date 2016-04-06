﻿using BankingSystem.Common.Data;

namespace BankingSystem.DataTier.Entities
{
    /// <summary>
    ///     Represents an account.
    /// </summary>
    /// <seealso cref="IAccount" />
    public class Account : IAccount
    {
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
        ///     Gets the customer.
        /// </summary>
        /// <value>
        ///     The customer.
        /// </value>
        public virtual ICustomer Customer { get; protected set; }

        /// <summary>
        ///     Gets the assigned bank card.
        /// </summary>
        /// <value>
        ///     The bank card.
        /// </value>
        public virtual IBankCard BankCard { get; protected set; }
    }
}