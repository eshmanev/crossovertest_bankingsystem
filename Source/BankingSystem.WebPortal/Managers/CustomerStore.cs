using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingSystem.Domain.Services;
using Microsoft.AspNet.Identity;

namespace BankingSystem.WebPortal.Managers
{
    /// <summary>
    /// Represents a store of customers.
    /// </summary>
    public class CustomerStore : IUserLoginStore<User, int>
    {
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerStore"/> class.
        /// </summary>
        /// <param name="customerService">The customer service.</param>
        public CustomerStore(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// This method is not supported.
        /// </summary>
        Task IUserStore<User, int>.CreateAsync(User user)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This method is not supported.
        /// </summary>
        Task IUserStore<User, int>.UpdateAsync(User user)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This method is not supported.
        /// </summary>
        Task IUserStore<User, int>.DeleteAsync(User user)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This method is not supported.
        /// </summary>
        Task IUserLoginStore<User, int>.AddLoginAsync(User user, UserLoginInfo login)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This method is not supported.
        /// </summary>
        Task IUserLoginStore<User, int>.RemoveLoginAsync(User user, UserLoginInfo login)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Searches a user with the given identifier.
        /// </summary>
        /// <param name="userId">The identifier.</param>
        /// <returns>A user or null.</returns>
        public Task<User> FindByIdAsync(int userId)
        {
            var customer = _customerService.FindCustomerById(userId);
            return Task.FromResult(customer != null ? new User(customer) : null);
        }

        /// <summary>
        /// Searches a user with the given user name.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>A user or null.</returns>
        public Task<User> FindByNameAsync(string userName)
        {
            var customer = _customerService.FindCustomerByName(userName);
            return Task.FromResult(customer != null ? new User(customer) : null);
        }

        /// <summary>
        /// Returns the linked accounts for this user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            IList<UserLoginInfo> logins = _customerService.GetAvailableLogins(user.Id)
                .Select(x => new UserLoginInfo(x.ProviderName, x.LoginKey))
                .ToArray();
            return Task.FromResult(logins);
        }

        /// <summary>
        /// Returns the user associated with this login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Task<User> FindAsync(UserLoginInfo login)
        {
            var customer = _customerService.FindCustomerByLogin(login.LoginProvider, login.ProviderKey);
            return Task.FromResult(customer != null ? new User(customer) : null);
        }

        public void Dispose()
        {
        }
    }
}