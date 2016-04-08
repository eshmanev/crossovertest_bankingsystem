using System;
using System.Collections.Generic;
using BankingSystem.Common.Data;

namespace BankingSystem.DataTier.Entities
{
    /// <summary>
    ///     Represents a merchant.
    /// </summary>
    /// <seealso cref="BankingSystem.Common.Data.IMerchant" />
    public class Merchant : IMerchant
    {
        private IList<Account> _accounts = new List<Account>();

        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public virtual Guid Id { get; protected set; }

        /// <summary>
        ///     Gets the merchant's name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public virtual string Name { get; protected set; }

        /// <summary>
        ///     Gets or sets the accounts.
        /// </summary>
        /// <value>
        ///     The accounts.
        /// </value>
        public virtual IEnumerable<IAccount> Accounts
        {
            get { return _accounts; }
            protected set { _accounts = (IList<Account>) value; }
        }
    }
}