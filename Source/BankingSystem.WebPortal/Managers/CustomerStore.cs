using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingSystem.Domain;
using Microsoft.AspNet.Identity;

namespace BankingSystem.WebPortal.Managers
{
    /// <summary>
    /// Represents a store of customers.
    /// </summary>
    public class CustomerStore : IUserLoginStore<User, int>, IUserLockoutStore<User, int>, IUserEmailStore<User, int>, IUserTwoFactorStore<User, int>
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
            return Task.FromResult(true);
        }

        /// <summary>
        /// This method is not supported.
        /// </summary>
        Task IUserStore<User, int>.DeleteAsync(User user)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Adds a user login with the specified provider and key
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="login">The user's login.</param>
        public Task AddLoginAsync(User user, UserLoginInfo login)
        {
            _customerService.AddCustomerLogin(user.Id, login.LoginProvider, login.ProviderKey);
            return Task.FromResult(true);
        }

        /// <summary>
        /// Removes the user login with the specified combination if it exists.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="login">The user's login.</param>
        public Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            _customerService.RemoveCustomerLogin(user.Id, login.LoginProvider, login.ProviderKey);
            return Task.FromResult(true);
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

        #region Implementation of IUserLockoutStore<User, int>

        Task<DateTimeOffset> IUserLockoutStore<User, int>.GetLockoutEndDateAsync(User user)
        {
            return Task.FromResult(DateTimeOffset.MinValue);
        }

        Task IUserLockoutStore<User, int>.SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            return Task.FromResult(false);
        }

        Task<int> IUserLockoutStore<User, int>.IncrementAccessFailedCountAsync(User user)
        {
            return Task.FromResult(0);
        }

        Task IUserLockoutStore<User, int>.ResetAccessFailedCountAsync(User user)
        {
            return Task.FromResult(false);
        }

        Task<int> IUserLockoutStore<User, int>.GetAccessFailedCountAsync(User user)
        {
            return Task.FromResult(0);
        }

        Task<bool> IUserLockoutStore<User, int>.GetLockoutEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }

        Task IUserLockoutStore<User, int>.SetLockoutEnabledAsync(User user, bool enabled)
        {
            return Task.FromResult(false);
        }

        #endregion

        #region Implementation of  IUserEmailStore<User, int>

        Task IUserEmailStore<User, int>.SetEmailAsync(User user, string email)
        {
            _customerService.UpdateCustomerEmail(user.Id, email);
            return Task.FromResult(true);
        }

        Task<string> IUserEmailStore<User, int>.GetEmailAsync(User user)
        {
            var customer = _customerService.FindCustomerById(user.Id);
            return Task.FromResult(customer?.Email);
        }

        Task<bool> IUserEmailStore<User, int>.GetEmailConfirmedAsync(User user)
        {
            return Task.FromResult(true);
        }

        Task IUserEmailStore<User, int>.SetEmailConfirmedAsync(User user, bool confirmed)
        {
            return Task.FromResult(true);
        }

        Task<User> IUserEmailStore<User, int>.FindByEmailAsync(string email)
        {
            var customer = _customerService.FindCustomerByEmail(email);
            return Task.FromResult(customer != null ? new User(customer) : null);
        }

        #endregion

        #region Implementation of IUserTwoFactorStore<User, int>

        Task IUserTwoFactorStore<User, int>.SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            return Task.FromResult(false);
        }

        Task<bool> IUserTwoFactorStore<User, int>.GetTwoFactorEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }

        #endregion
    }
}