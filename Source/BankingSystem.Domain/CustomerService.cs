using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BankingSystem.Data;
using BankingSystem.DAL;
using BankingSystem.DAL.Entities;

namespace BankingSystem.Domain
{
    /// <summary>
    ///     Represents a customer service.
    /// </summary>
    /// <seealso cref="ICustomerService" />
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
        ///     Searches for a customer with the given email address.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        ///     An instance of the <see cref="ICustomer" /> or null.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ICustomer FindCustomerByEmail(string email)
        {
            return _databaseContext.Customers.Filter(x => x.Email == email).FirstOrDefault();
        }

        /// <summary>
        ///     Searches for a customer with the given ID.
        /// </summary>
        /// <param name="userId">The identifier.</param>
        /// <returns>
        ///     An instance of the <see cref="ICustomer" /> or null.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ICustomer FindCustomerById(int userId)
        {
            return _databaseContext.Customers.GetById(userId);
        }

        /// <summary>
        ///     Gets a list of the available logins for the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///     A list of the <see cref="ILoginInfo" /> objects.
        /// </returns>
        public IList<ILoginInfo> GetAvailableLogins(int userId)
        {
            var customer = _databaseContext.Customers.GetById(userId);
            return customer?.Logins.ToArray() ?? new ILoginInfo[0];
        }

        /// <summary>
        ///     Adds the customer login.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="loginKey">The login key.</param>
        /// <returns>
        ///     A created login info.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ILoginInfo AddCustomerLogin(int userId, string providerName, string loginKey)
        {
            var customer = _databaseContext.Customers.GetById(userId);
            if (customer == null)
                throw new ArgumentException("Invalid user identifier", nameof(userId));

            var loginInfo = new LoginInfo(providerName, loginKey);
            customer.AddLogin(loginInfo);
            _databaseContext.Customers.Update(customer);
            _databaseContext.Commit();
            return loginInfo;
        }

        /// <summary>
        ///     Removes the customer login if it exists; otherwise does nothing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="loginKey">The login key.</param>
        /// <returns>
        ///     A removed login info.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ILoginInfo RemoveCustomerLogin(int userId, string providerName, string loginKey)
        {
            var customer = _databaseContext.Customers.GetById(userId);
            if (customer == null)
                throw new ArgumentException("Invalid user identifier", nameof(userId));

            var loginInfo = customer.Logins.FirstOrDefault(x => x.ProviderName == providerName && x.LoginKey == loginKey);
            if (loginInfo != null)
            {
                customer.RemoveLogin(loginInfo);
                _databaseContext.Customers.Update(customer);
                _databaseContext.Commit();
            }

            return loginInfo;
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

        /// <summary>
        ///     Validates the pair username/password.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        ///     true if validate is successfull; otherwise false.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool ValidatePassword(string userName, string password)
        {
            var customer = FindCustomerByName(userName);
            if (customer == null)
                return false;

            var provider = new SHA256CryptoServiceProvider();
            var bytes = Encoding.ASCII.GetBytes(password);
            var passwordHashBytes = provider.ComputeHash(bytes);

            var strBuilder = new StringBuilder();
            foreach (var ch in passwordHashBytes)
            {
                //change it into 2 hexadecimal digits for each byte
                strBuilder.Append(ch.ToString("x2"));
            }
            var passwordHash = strBuilder.ToString();
            return customer.PasswordHash == passwordHash;
        }

        /// <summary>
        ///     Updates the customer's email address.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="email">The email.</param>
        public void UpdateCustomerEmail(int userId, string email)
        {
            var customer = FindCustomerById(userId);
            if (customer == null)
                return;

            customer.Email = email;
            _databaseContext.Customers.Update(customer);
            _databaseContext.Commit();
        }
    }
}