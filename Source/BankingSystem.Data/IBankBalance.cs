﻿namespace BankingSystem.Data
{
    /// <summary>
    ///     Defines a bank balance.
    /// </summary>
    public interface IBankBalance
    {
        /// <summary>
        ///     Gets or sets the tatal balance.
        /// </summary>
        /// <value>
        ///     The total balance.
        /// </value>
        decimal TotalBalance { get; set; }
    }
}