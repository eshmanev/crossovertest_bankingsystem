using System;
using BankingSystem.Domain;
using BankingSystem.WebPortal;
using BankingSystem.WebPortal.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Practices.Unity;
using Owin;
using Owin.Security.Providers.LinkedIn;

[assembly: OwinStartup(typeof (Startup))]
namespace BankingSystem.WebPortal
{
    /// <summary>
    /// Defines an entry point for OWIN application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        /// <summary>
        /// Configures the application authentication.
        /// </summary>
        /// <param name="app">The application.</param>
        private void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            var customerService = UnityConfig.Container.Resolve<ICustomerService>();
            app.CreatePerOwinContext<CustomerManager>((options, ctx) => CustomerManager.Create(options, ctx, customerService));
            app.CreatePerOwinContext<CustomerSignInManager>((options, ctx) => CustomerSignInManager.Create(options, ctx, customerService));

            //// Enable the application to use a cookie to store information for the signed in user
            //// and to use a cookie to temporarily store information about a user logging in with a third party login provider
            //// Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<CustomerManager, User, int>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) => user.GetCliemsIdentityAsync(),
                        getUserIdCallback: (claims) => claims.IsAuthenticated ? claims.GetUserId<int>() : 0)
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseFacebookAuthentication(appId: "473903399469595", appSecret: "7e4adda0d43e0e1438f8e453a45743ba");
            app.UseLinkedInAuthentication(clientId: "77odrj1h7nmx5g", clientSecret: "2YwB0LFyKTyGnEK3");
            app.UseTwitterAuthentication(consumerKey: "DRbZmgJnVqhxOLHRgR8bISyW9",
                consumerSecret: "NSysx9rCf6zyYdPOm4kwIneu4O1PfKZ1pbMkbratB8PbaBPPhD");
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "409477153620-7eq2ke1tpfhb32p168lcmo1815vdmh0e.apps.googleusercontent.com",
                ClientSecret = "nK-vsKE-bjtXd6gwbO1OIAWC"
            });
        }
    }
}