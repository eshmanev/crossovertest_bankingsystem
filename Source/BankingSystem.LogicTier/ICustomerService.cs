using System.Collections.Generic;
using BankingSystem.Common.Data;

namespace BankingSystem.LogicTier
{
    /// <summary>
    ///     Defines a service of customers.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        ///     Searches for a customer with the given user name.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>An instance of the <see cref="ICustomer" /> or null.</returns>
        ICustomer FindCustomerByName(string userName);

        /// <summary>
        ///     Searches for a customer with the given email address.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>An instance of the <see cref="ICustomer" /> or null.</returns>
        ICustomer FindCustomerByEmail(string email);

        /// <summary>
        ///     Searches for a customer with the given ID.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>An instance of the <see cref="ICustomer" /> or null.</returns>
        ICustomer FindCustomerById(int userId);

        /// <summary>
        ///     Searches for a customer who has the specified account.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns>An instance of the <see cref="ICustomer" /> or null.</returns>
        ICustomer FindCustomerByAccount(string accountNumber);

        /// <summary>
        ///     Gets a list of the available logins for the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>A list of the <see cref="ILoginInfo" /> objects.</returns>
        IList<ILoginInfo> GetAvailableLogins(int userId);

        /// <summary>
        ///     Adds the customer login.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="loginKey">The login key.</param>
        /// <returns>A created login info.</returns>
        ILoginInfo AddCustomerLogin(int userId, string providerName, string loginKey);

        /// <summary>
        ///     Removes the customer login if it exists; otherwise does nothing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="loginKey">The login key.</param>
        /// <returns>A removed login info.</returns>
        ILoginInfo RemoveCustomerLogin(int userId, string providerName, string loginKey);

        /// <summary>
        ///     Searches for a customer by social account.
        /// </summary>
        /// <param name="providerName">Name of the login provider.</param>
        /// <param name="loginKey">The login key.</param>
        /// <returns>An instance of the <see cref="ICustomer" /> or null.</returns>
        ICustomer FindCustomerByLogin(string providerName, string loginKey);

        /// <summary>
        ///     Validates the pair username/password.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>true if validate is successfull; otherwise false.</returns>
        bool ValidatePassword(string userName, string password);

        /// <summary>
        ///     Updates the customer's email address.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="email">The email.</param>
        void UpdateCustomerEmail(int userId, string email);
    }
}