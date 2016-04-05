using System.Collections.Generic;
using BankingSystem.Data;

namespace BankingSystem.DAL.Entities
{
    /// <summary>
    /// Represents a customer.
    /// </summary>
    /// <seealso cref="BankingSystem.Data.ICustomer" />
    public class Customer : ICustomer
    {
        private IList<Account> _accounts = new List<Account>();
        private IList<LoginInfo> _logins = new List<LoginInfo>();

        /// <summary>
        ///     Gets the entity identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public virtual int Id { get; protected set; }

        /// <summary>
        ///     Gets the unique username.
        /// </summary>
        /// <value>
        ///     The unique username.
        /// </value>
        public virtual string UserName { get; protected set; }

        /// <summary>
        ///     Gets the first name.
        /// </summary>
        /// <value>
        ///     The first name.
        /// </value>
        public virtual string FirstName { get; protected set; }

        /// <summary>
        ///     Gets the last name.
        /// </summary>
        /// <value>
        ///     The last name.
        /// </value>
        public virtual string LastName { get; protected set; }

        /// <summary>
        ///     Gets a collection of the customer's accounts.
        /// </summary>
        /// <value>
        ///     The accounts.
        /// </value>
        public virtual IEnumerable<IAccount> Accounts
        {
            get { return _accounts; }
            set { _accounts = (IList<Account>) value; }
        }

        /// <summary>
        ///     Gets a collection of the customer's logins.
        /// </summary>
        /// <value>
        ///     The logins.
        /// </value>
        public virtual IEnumerable<ILoginInfo> Logins
        {
            get { return _logins; }
            set { _logins = (IList<LoginInfo>) value; }
        }
    }
}