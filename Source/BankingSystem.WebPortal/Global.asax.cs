using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BankingSystem.WebPortal
{
    /// <summary>
    /// Contains application handlers.
    /// </summary>
    /// <seealso cref="System.Web.HttpApplication" />
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Handles application start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.ConfigureContainer();
        }

        /// <summary>
        /// Handles application stop.
        /// </summary>
        protected void Application_Stop()
        {
            UnityConfig.Container.Dispose();
        }
    }
}