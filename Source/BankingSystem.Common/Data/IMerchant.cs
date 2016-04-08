using System;
using System.Collections.Generic;

namespace BankingSystem.Common.Data
{
    /// <summary>
    ///     Defines a merchant
    /// </summary>
    public interface IMerchant
    {
        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        Guid Id { get; }

        /// <summary>
        ///     Gets the merchant's name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        string Name { get; }

        /// <summary>
        ///     Gets the merchant's accounts.
        /// </summary>
        /// <value>
        ///     The accounts.
        /// </value>
        IEnumerable<IAccount> Accounts { get; }
    }
}