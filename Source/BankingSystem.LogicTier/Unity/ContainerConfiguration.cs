using System;
using System.Linq;
using System.Reflection;
using BankingSystem.DataTier;
using BankingSystem.DataTier.Repositories;
using BankingSystem.DataTier.Repositories.Impl;
using BankingSystem.DataTier.Session;
using BankingSystem.LogicTier.Impl;
using BankingSystem.LogicTier.Utils;
using Microsoft.Practices.Unity;

namespace BankingSystem.LogicTier.Unity
{
    /// <summary>
    ///     Contains configuration of services.
    /// </summary>
    public static class ContainerConfiguration
    {
        /// <summary>
        ///     Configures the services.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="singletonLifetimeManager">The singleton lifetime manager.</param>
        public static void ConfigureServices(this IUnityContainer container, Func<LifetimeManager> singletonLifetimeManager)
        {
            // data tier
            container.RegisterType<IDatabaseContext, DatabaseContext>(singletonLifetimeManager());
            container.RegisterType(typeof (IRepository<>), typeof (Repository<>), singletonLifetimeManager());
            container.RegisterType<ISessionFactoryHolder, SessionFactoryHolder>(new ContainerControlledLifetimeManager());

            // logic tier
            container.RegisterType<ISmtpFactory, SmtpFactory>();
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<IExchangeRateService, ExchangeRateService>();
            container.RegisterType<IBankBalanceService, BankBalanceService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IBankCardService, BankCardService>();
            container.RegisterType<IMerchantService, MerchantService>();
            container.RegisterType<IJournalService, JournalService>();
        }

        private static void RegisterOptional<TFrom, TTo>(this IUnityContainer container) where TTo : TFrom
        {
            if (container.IsRegistered<TFrom>())
                return;
            container.RegisterType<TFrom, TTo>();
        }

        private static bool IsRegistered<T>(this IUnityContainer container)
        {
            var typeToCheck = typeof (T);
            var genericTypeToCheck = typeToCheck.GetTypeInfo().IsGenericType
                         ? typeToCheck.GetGenericTypeDefinition()
                         : null;

            var registration = container.Registrations.Where(r => (r.RegisteredType.GetTypeInfo().IsGenericTypeDefinition
                ? r.RegisteredType == genericTypeToCheck
                : r.RegisteredType == typeToCheck));
            return registration.FirstOrDefault() != null;
        }
    }
}