using System.Collections.Generic;

namespace BankingSystem.Domain.Impl
{
    /// <summary>
    ///     Represents a customer.
    /// </summary>
    /// <seealso cref="ICustomer" />
    internal abstract class CustomerBase : ICustomer
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
        ///     Gets the customer's email address.
        /// </summary>
        /// <value>
        ///     The email.
        /// </value>
        public virtual string Email { get; set; }

        /// <summary>
        ///     Gets the password hash.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        ///     Gets a collection of the customer's accounts.
        /// </summary>
        /// <value>
        ///     The accounts.
        /// </value>
        public virtual IEnumerable<IAccount> Accounts
        {
            get { return _accounts; }
            protected set { _accounts = (IList<Account>) value; }
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
            protected set { _logins = (IList<LoginInfo>) value; }
        }

        /// <summary>
        ///     Adds the login.
        /// </summary>
        /// <param name="login">The login.</param>
        public virtual void AddLogin(ILoginInfo login)
        {
            var loginInfo = (LoginInfo) login;
            if (loginInfo != null && !_logins.Contains(loginInfo))
            {
                _logins.Add(loginInfo);
                loginInfo.Customer = this;
            }
        }

        /// <summary>
        ///     Removes the login.
        /// </summary>
        /// <param name="login">The login.</param>
        public virtual void RemoveLogin(ILoginInfo login)
        {
            var loginInfo = (LoginInfo) login;
            if (loginInfo != null && _logins.Contains(loginInfo))
            {
                _logins.Remove(loginInfo);
                loginInfo.Customer = null;
            }
        }

        /// <summary>
        ///     Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public abstract void Accept(ICustomerVisitor visitor);
    }
}