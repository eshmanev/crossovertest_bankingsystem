using BankingSystem.Common.Data;

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
        /// <returns></returns>
        ICustomer FindByUserName(string userName);

        /// <summary>
        ///     Searches for a customer with the given email address.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        ICustomer FindByEmail(string email);
    }
}