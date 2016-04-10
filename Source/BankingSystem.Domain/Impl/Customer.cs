﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingSystem.Domain.Impl
{
    /// <summary>
    ///     Represents a customer.
    /// </summary>
    /// <seealso cref="ICustomer" />
    public abstract class CustomerBase : ICustomer
    {
        private IList<Account> _accounts = new List<Account>();
        private IList<LoginInfo> _logins = new List<LoginInfo>();

        protected CustomerBase()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomerBase" /> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="email">The email.</param>
        /// <param name="passwordHash">The password hash.</param>
        protected CustomerBase(string userName, string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentNullException(nameof(passwordHash));

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

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
        ///     Adds the account.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="currency">The currency.</param>
        /// <returns></returns>
        public virtual IAccount AddAccount(string accountNumber, string currency)
        {
            if (_accounts.Any(x => x.AccountNumber == accountNumber))
                throw new ArgumentException("Account already exists", nameof(accountNumber));

            var account = new Account(accountNumber, currency, this);
            _accounts.Add(account);
            return account;
        }

        /// <summary>
        /// Removes the account.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        public virtual void RemoveAccount(string accountNumber)
        {
            var account = _accounts.FirstOrDefault(x => x.AccountNumber != accountNumber);
            if (account == null)
                throw new ArgumentException("Account does not exist", nameof(accountNumber));

            _accounts.Remove(account);
        }

        /// <summary>
        ///     Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public abstract void Accept(ICustomerVisitor visitor);
    }
}