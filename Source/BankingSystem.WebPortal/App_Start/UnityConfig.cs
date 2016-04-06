using System.Linq;
using System.Web.Mvc;
using BankingSystem.DataTier;
using BankingSystem.DataTier.Session;
using BankingSystem.LogicTier;
using BankingSystem.LogicTier.Impl;
using BankingSystem.WebPortal.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
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

            // services
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<IExchangeRateService, ExchangeRateService>();
            container.RegisterType<IBankBalanceService, BankBalanceService>();

            // hubs
            container.RegisterInstance(GlobalHost.ConnectionManager);
            container.RegisterHubContext<AccountHub>(HubNames.AccountHub);

            return container;
        }

        private static void RegisterHubContext<T>(this IUnityContainer container, string hubName) where T : IHub
        {
            container.RegisterType<IHubConnectionContext<dynamic>>(hubName, new InjectionFactory(_ => GlobalHost.ConnectionManager.GetHubContext<T>().Clients));
        }
    }
}