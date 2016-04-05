using System.Linq;
using System.Web.Mvc;
using BankingSystem.DAL;
using BankingSystem.DAL.Session;
using BankingSystem.Domain;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

namespace BankingSystem.WebPortal
{
    /// <summary>
    /// Contains a configuration of unity container.
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public static IUnityContainer Container { get; private set; }

        /// <summary>
        /// Configures the container.
        /// </summary>
        public static void ConfigureContainer()
        {
            // build container
            Container = BuildUnityContainer();

            // extra settings
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(Container));
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IDatabaseContext, DatabaseContext>(new PerRequestLifetimeManager());
            container.RegisterType(typeof (IRepository<>), typeof (Repository<>), new PerRequestLifetimeManager());
            container.RegisterType<ISessionFactoryHolder, SessionFactoryHolder>(new ContainerControlledLifetimeManager());

            container.RegisterType<ICustomerService, CustomerService>();
            return container;
        }
    }
}