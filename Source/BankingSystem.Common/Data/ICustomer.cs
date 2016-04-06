using System.Collections.Generic;

namespace BankingSystem.Common.Data
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
        ///     Gets the first name.
        /// </summary>
        /// <value>
        ///     The first name.
        /// </value>
        string FirstName { get; }

        /// <summary>
        ///     Gets the last name.
        /// </summary>
        /// <value>
        ///     The last name.
        /// </value>
        string LastName { get; }

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
    }
}