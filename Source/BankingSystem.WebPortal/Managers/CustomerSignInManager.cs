using System.Threading.Tasks;
using BankingSystem.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace BankingSystem.WebPortal.Managers
{
    /// <summary>
    ///     Represents a custom implementation of SignIn Manager.
    /// </summary>
    public class CustomerSignInManager : SignInManager<User, int>
    {
        private readonly ICustomerService _customerService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomerSignInManager" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="authenticationManager">The authentication manager.</param>
        /// <param name="customerService">The customer service.</param>
        public CustomerSignInManager(UserManager<User, int> userManager, IAuthenticationManager authenticationManager, ICustomerService customerService)
            : base(userManager, authenticationManager)
        {
            _customerService = customerService;
        }

        /// <summary>
        ///     Creates the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="context">The context.</param>
        /// <param name="customerService">The customer service.</param>
        /// <returns></returns>
        public static CustomerSignInManager Create(IdentityFactoryOptions<CustomerSignInManager> options, IOwinContext context, ICustomerService customerService)
        {
            return new CustomerSignInManager(context.GetUserManager<CustomerManager>(), context.Authentication,
                customerService);
        }

        /// <summary>
        ///     Sign in the user in using the user name and password
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <param name="isPersistent">True to create a persistent cookie.</param>
        /// <param name="shouldLockout">Not used.</param>
        /// <returns>A status.</returns>
        public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            if (UserManager == null)
                return SignInStatus.Failure;

            if (!_customerService.ValidatePassword(userName, password))
                return SignInStatus.Failure;

            var customer = _customerService.FindCustomerByName(userName);
            var user = new User(customer);
            await SignInAsync(user, isPersistent, false);
            return SignInStatus.Success;
        }
    }
}