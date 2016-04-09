using System;
using BankingSystem.Domain;

namespace BankingSystem.DataTier.Repositories
{
    /// <summary>
    ///     Defines a repository of customers.
    /// </summary>
    public interface ICustomerRepository : IRepository<ICustomer>
    {
        /// <summary>
        ///     Searches for a customer with the given name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>A customer or null.</returns>
        ICustomer FindByUserName(string userName);

        /// <summary>
        ///     Searches for a customer with the given email address.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>A customer or null.</returns>
        ICustomer FindByEmail(string email);

        /// <summary>
        ///     Searches for a customer who has the specified account.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns>A customer or null.</returns>
        ICustomer FindByAccount(string accountNumber);

        /// <summary>
        ///     Searches for a merchant with the given merchant identifier.
        /// </summary>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <returns>A merchant or null.</returns>
        IMerchant FindByMerchantId(Guid merchantId);
    }
}