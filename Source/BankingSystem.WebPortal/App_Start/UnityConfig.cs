using System;
using System.Linq;
using System.Reflection;
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
        private static Func<LifetimeManager> _perRequestManagerFactory = () => new PerRequestLifetimeManager();
        
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
        ///     Sets a factory that provides per request lifetime manager.
        /// </summary>
        /// <param name="perRequestManagerFactory">The per request manager factory.</param>
        public static void SetPerRequestManagerFactory(Func<LifetimeManager> perRequestManagerFactory)
        {
            _perRequestManagerFactory = perRequestManagerFactory;
        }

        /// <summary>
        ///     Builds the unity container.
        /// </summary>
        /// <returns></returns>
        private static IUnityContainer BuildUnityContainer()
        {
            // configure services
            var container = new UnityContainer();
            container.ConfigureServices(_perRequestManagerFactory);

            // override service registrations

            container.RegisterType<IAccountService, AccountServiceDecorator>(
                container.Decorate<IAccountService>(s => new AccountServiceDecorator(s, container.Resolve<IHubConnectionContext<dynamic>>())));

            container.RegisterType<IJournalService, JournalServiceDecorator>(
                container.Decorate<IJournalService>(s => new JournalServiceDecorator(s, container.Resolve<IHubConnectionContext<dynamic>>())));

            // configure app
            container.RegisterInstance(GlobalHost.ConnectionManager);
            container.RegisterHubContext<NotificationHub>();

            return container;
        }

        /// <summary>
        ///     Registers the hub context.
        /// </summary>
        /// <typeparam name="T">The type of the hub.</typeparam>
        /// <param name="container">The container.</param>
        private static void RegisterHubContext<T>(this IUnityContainer container) where T : IHub
        {
            container.RegisterType<IHubConnectionContext<dynamic>>(new InjectionFactory(_ => GlobalHost.ConnectionManager.GetHubContext<T>().Clients));
        }

        private static InjectionFactory Decorate<T>(this IUnityContainer container, Func<T, T> createMethod)
        {
            // find original type
            var typeToCheck = typeof (T);
            var genericTypeToCheck = typeToCheck.GetTypeInfo().IsGenericType
                ? typeToCheck.GetGenericTypeDefinition()
                : null;
            var registration = container.Registrations.Where(r => (r.RegisteredType.GetTypeInfo().IsGenericTypeDefinition
                ? r.RegisteredType == genericTypeToCheck
                : r.RegisteredType == typeToCheck));
            var originalType = registration.First().MappedToType;

            // create decorator
            return new InjectionFactory(c => createMethod((T) container.Resolve(originalType)));
        }
    }
}