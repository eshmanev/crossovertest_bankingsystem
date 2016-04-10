using System;
using System.Configuration;
using BankingSystem.LogicTier;
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
            app.CreatePerOwinContext<CustomerManager>((options, ctx) => CustomerManager.Create(options, ctx, UnityConfig.Container.Resolve<ICustomerService>()));
            app.CreatePerOwinContext<CustomerSignInManager>((options, ctx) => CustomerSignInManager.Create(options, ctx, UnityConfig.Container.Resolve<ICustomerService>()));

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

            app.UseFacebookAuthentication(appId: ConfigurationManager.AppSettings["FacebookAppId"], appSecret: ConfigurationManager.AppSettings["FacebookSecret"]);
            app.UseLinkedInAuthentication(clientId: ConfigurationManager.AppSettings["LinkedinClientId"], clientSecret: ConfigurationManager.AppSettings["LinkedinSecret"]);
            app.UseTwitterAuthentication(consumerKey: ConfigurationManager.AppSettings["TwitterConsumerKey"], consumerSecret: ConfigurationManager.AppSettings["TwitterConsumerSecret"]);
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = ConfigurationManager.AppSettings["GoogleClientId"],
                ClientSecret = ConfigurationManager.AppSettings["GoogleSecret"]
            });
            app.MapSignalR();
        }
    }
}