using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace BankingSystem.WebPortal.Managers
{
    public class CustomerSignInManager : SignInManager<User, int>
    {
        public CustomerSignInManager(UserManager<User, int> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public static CustomerSignInManager Create(IdentityFactoryOptions<CustomerSignInManager> options, IOwinContext context)
        {
            return new CustomerSignInManager(context.GetUserManager<CustomerManager>(), context.Authentication);
        }
    }
}