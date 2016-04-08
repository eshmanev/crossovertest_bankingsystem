using System.Web.Mvc;
using System.Web.Routing;

namespace BankingSystem.WebPortal
{
    /// <summary>
    ///     Defines a configuration of ASP.NET routes.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        ///     Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "OnlinePaymentEntryPoint",
                url: "onlinepayment/{merchant}/{currency}/{sum}",
                defaults: new {controller = "OnlinePayment", action = "Index"});

            routes.MapRoute(
               name: "OnlinePaymentProcessing",
               url: "onlinepayment",
               defaults: new { controller = "OnlinePayment", action = "Payment" });

            routes.MapRoute(
                name: "Login",
                url: "Account/Login",
                defaults: new {controller = "Auth", action = "Login"});

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}