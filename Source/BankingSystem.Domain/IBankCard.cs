﻿using System;

namespace BankingSystem.Domain
{
    /// <summary>
    /// Defines a bank card.
    /// </summary>
    public interface IBankCard
    {
        /// <summary>
        /// Gets the CSV security code.
        /// </summary>
        /// <value>
        /// The CSV security code.
        /// </value>
        string CsvCode { get; }

        /// <summary>
        /// Gets the name of the card holder.
        /// </summary>
        /// <value>
        /// The name of the card holder.
        /// </value>
        string CardHolder { get; }

        /// <summary>
        /// Gets the card number.
        /// </summary>
        /// <value>
        /// The card number.
        /// </value>
        string CardNumber { get; }

        /// <summary>
        /// Gets the expiration month.
        /// </summary>
        /// <value>
        /// The expiration month.
        /// </value>
        int ExpirationMonth { get; }

        /// <summary>
        /// Gets the expiration year.
        /// </summary>
        /// <value>
        /// The expiration year.
        /// </value>
        int ExpirationYear { get; }
    }
}