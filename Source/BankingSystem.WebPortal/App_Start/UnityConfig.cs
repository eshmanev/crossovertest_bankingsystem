using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using BankingSystem.LogicTier;
using BankingSystem.LogicTier.Unity;
using BankingSystem.WebPortal.Hubs;
using BankingSystem.WebPortal.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Practices.Unity;

namespace BankingSystem.WebPortal
{
    /// <summary>
    ///     Contains a configuration of unity container.
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        ///     Gets the container.
        /// </summary>
        /// <value>
        ///     The container.
        /// </value>
        public static IUnityContainer Container { get; private set; }

        /// <summary>
        ///     Configures the container.
        /// </summary>
        public static void ConfigureContainer()
        {
            // build container
            Container = BuildUnityContainer();

            // MVC unity configuration
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new Microsoft.Practices.Unity.Mvc.UnityFilterAttributeFilterProvider(Container));
            DependencyResolver.SetResolver(new Microsoft.Practices.Unity.Mvc.UnityDependencyResolver(Container));

            // WebAPI unity configuration
            GlobalConfiguration.Configuration.DependencyResolver = new Microsoft.Practices.Unity.WebApi.UnityDependencyResolver(Container);
        }

        /// <summary>
        ///     Builds the unity container.
        /// </summary>
        /// <returns></returns>
        private static IUnityContainer BuildUnityContainer()
        {
            // configure services
            var serviceContainer = new UnityContainer();
            serviceContainer.ConfigureServices(() => new PerRequestLifetimeManager());

            // configure app
            var appContainer = serviceContainer.CreateChildContainer();
            appContainer.RegisterType<IAccountService>(
                new InjectionFactory(c => new AccountServiceWithNotificationsDecorator(serviceContainer.Resolve<IAccountService>(), c.Resolve<IHubConnectionContext<dynamic>>())));

            // hubs
            appContainer.RegisterInstance(GlobalHost.ConnectionManager);
            appContainer.RegisterHubContext<AccountHub>(HubNames.AccountHub);

            return appContainer;
        }

        /// <summary>
        ///     Registers the hub context.
        /// </summary>
        /// <typeparam name="T">The type of the hub.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="hubName">Name of the hub.</param>
        private static void RegisterHubContext<T>(this IUnityContainer container, string hubName) where T : IHub
        {
            container.RegisterType<IHubConnectionContext<dynamic>>(hubName, new InjectionFactory(_ => GlobalHost.ConnectionManager.GetHubContext<T>().Clients));
        }
    }
}