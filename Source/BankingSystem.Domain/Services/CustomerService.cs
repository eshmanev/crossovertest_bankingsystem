using System.Collections.Generic;
using System.Linq;
using BankingSystem.Data;
using BankingSystem.DAL;

namespace BankingSystem.Domain.Services
{
    /// <summary>
    ///     Represents a customer service.
    /// </summary>
    /// <seealso cref="BankingSystem.Domain.Services.ICustomerService" />
    public class CustomerService : ICustomerService
    {
        private readonly IDatabaseContext _databaseContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomerService" /> class.
        /// </summary>
        /// <param name="databaseContext">The database context.</param>
        public CustomerService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        /// <summary>
        ///     Searches for a customer with the given name.
        /// </summary>
        /// <param name="userName">The name.</param>
        /// <returns>
        ///     An instance of the <see cref="ICustomer" /> or null.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ICustomer FindCustomerByName(string userName)
        {
            return _databaseContext.Customers.Filter(x => x.UserName == userName).FirstOrDefault();
        }

        /// <summary>
        ///     Searches for a customer with the given ID.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     An instance of the <see cref="ICustomer" /> or null.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ICustomer FindCustomerById(int id)
        {
            return _databaseContext.Customers.GetById(id);
        }

        /// <summary>
        ///     Gets a list of the available logins for the specified user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>
        ///     A list of the <see cref="ILoginInfo" /> objects.
        /// </returns>
        public IList<ILoginInfo> GetAvailableLogins(int id)
        {
            var customer = _databaseContext.Customers.GetById(id);
            return customer?.Logins.ToArray() ?? new ILoginInfo[0];
        }

        /// <summary>
        ///     Searches for a customer by social account.
        /// </summary>
        /// <param name="providerName">Name of the login provider.</param>
        /// <param name="loginKey">The login key.</param>
        /// <returns>
        ///     An instance of the <see cref="ICustomer" /> or null.
        /// </returns>
        public ICustomer FindCustomerByLogin(string providerName, string loginKey)
        {
            var info = _databaseContext.LoginInfos
                .Filter(x => x.ProviderName == providerName && x.LoginKey == loginKey)
                .FirstOrDefault();

            return info?.Customer;
        }
    }
}