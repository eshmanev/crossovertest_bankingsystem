using System.Collections.Generic;
using BankingSystem.Data;
using BankingSystem.DAL.Entities;

namespace BankingSystem.Domain.Services
{
    /// <summary>
    /// Defines a service of customers.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Searches for a customer with the given user name.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>An instance of the <see cref="ICustomer"/> or null.</returns>
        ICustomer FindCustomerByName(string userName);

        /// <summary>
        /// Searches for a customer with the given ID.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>An instance of the <see cref="ICustomer"/> or null.</returns>
        ICustomer FindCustomerById(int id);

        /// <summary>
        /// Gets a list of the available logins for the specified user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>A list of the <see cref="ILoginInfo"/> objects.</returns>
        IList<ILoginInfo> GetAvailableLogins(int id);

        /// <summary>
        /// Searches for a customer by social account.
        /// </summary>
        /// <param name="providerName">Name of the login provider.</param>
        /// <param name="loginKey">The login key.</param>
        /// <returns>An instance of the <see cref="ICustomer"/> or null.</returns>
        ICustomer FindCustomerByLogin(string providerName, string loginKey);
    }
}