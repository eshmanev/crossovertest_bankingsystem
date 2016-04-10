using System.Collections.Generic;

namespace BankingSystem.Domain
{
    /// <summary>
    ///     Defines a customer.
    /// </summary>
    public interface ICustomer
    {
        /// <summary>
        ///     Gets the entity identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        int Id { get; }

        /// <summary>
        ///     Gets the unique username.
        /// </summary>
        /// <value>
        ///     The unique username.
        /// </value>
        string UserName { get; }

        /// <summary>
        ///     Gets the customer's email address.
        /// </summary>
        /// <value>
        ///     The email.
        /// </value>
        string Email { get; set; }

        /// <summary>
        ///     Gets the password hash.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        string PasswordHash { get; }

        /// <summary>
        ///     Gets a collection of the customer's accounts.
        /// </summary>
        /// <value>
        ///     The accounts.
        /// </value>
        IEnumerable<IAccount> Accounts { get; }

        /// <summary>
        ///     Gets a collection of the customer's logins.
        /// </summary>
        /// <value>
        ///     The logins.
        /// </value>
        IEnumerable<ILoginInfo> Logins { get; }

        /// <summary>
        ///     Adds the login.
        /// </summary>
        /// <param name="login">The login.</param>
        void AddLogin(ILoginInfo login);

        /// <summary>
        ///     Removes the login.
        /// </summary>
        /// <param name="login">The login.</param>
        void RemoveLogin(ILoginInfo login);

        /// <summary>
        ///     Adds the account.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="currency">The currency.</param>
        /// <returns></returns>
        IAccount AddAccount(string accountNumber, string currency);

        /// <summary>
        ///     Removes the account.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        void RemoveAccount(string accountNumber);

        /// <summary>
        ///     Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        void Accept(ICustomerVisitor visitor);
    }
}