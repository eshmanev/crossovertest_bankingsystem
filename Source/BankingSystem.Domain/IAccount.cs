﻿namespace BankingSystem.Domain
{
    /// <summary>
    /// Defines an account.
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// Gets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        string AccountNumber { get; }

        /// <summary>
        /// Gets the assigned bank card.
        /// </summary>
        /// <value>
        /// The bank card.
        /// </value>
        IBankCard BankCard { get; }
    }
}