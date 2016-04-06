using System;
using System.Threading.Tasks;
using BankingSystem.LogicTier;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace BankingSystem.WebPortal.Managers
{
    public class CustomerManager : UserManager<User, int>
    {
        public CustomerManager(IUserStore<User, int> store) : base(store)
        {
        }

        public static CustomerManager Create(IdentityFactoryOptions<CustomerManager> options, IOwinContext context, ICustomerService customerService)
        {
            var manager = new CustomerManager(new CustomerStore(customerService));
            
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}