using System.Web.Http;
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
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(ApiRouteConfig.RegisterRoutes);
            UnityConfig.ConfigureContainer();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
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