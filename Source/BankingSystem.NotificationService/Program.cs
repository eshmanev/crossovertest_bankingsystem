using BankingSystem.Common.Messages;
using BankingSystem.LogicTier.Unity;
using BankingSystem.NotificationService.Handlers;
using Microsoft.Practices.Unity;
using Topshelf;
using Topshelf.Unity;

namespace BankingSystem.NotificationService
{
    /// <summary>
    ///     Represents the program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        ///     Entry point.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main()
        {
            HostFactory.Run(x =>
            {
                // configure recovery
                x.EnableServiceRecovery(
                    recovery =>
                    {
                        recovery.RestartService(0);
                        recovery.SetResetPeriod(1);
                    });

                // configure container
                x.UseUnityContainer(BuildContainer());

                // configure service
                x.Service<ProgramControl>(s =>
                {
                    s.ConstructUsingUnityContainer();
                    s.WhenStarted((service, control) => service.Start(control));
                    s.WhenStopped((service, control) => service.Stop(control));
                });
            });
        }

        /// <summary>
        ///     Builds the container.
        /// </summary>
        /// <returns>An instance of <see cref="IUnityContainer"/>.</returns>
        private static IUnityContainer BuildContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<ISettings, Settings>();
            container.ConfigureServices(() => new ContainerControlledLifetimeManager());

            // handlers
            container.RegisterType<IHandler<BalanceChangedMessage>, AccountHandler>();

            return container;
        }
    }
}