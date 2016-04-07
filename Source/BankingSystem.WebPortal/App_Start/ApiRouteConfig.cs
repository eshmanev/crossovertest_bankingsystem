using System.Web.Http;

namespace BankingSystem.WebPortal
{
    /// <summary>
    ///     Defines a configuration of HTTP routes.
    /// </summary>
    public class ApiRouteConfig
    {
        /// <summary>
        ///     Registers the routes.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void RegisterRoutes(HttpConfiguration config)
        {
            // Web API routes
            config.Routes.MapHttpRoute(
                    "API Default", "api/{controller}/{action}",
                    new { id = RouteParameter.Optional });

            config.MapHttpAttributeRoutes();
        }
    }
}