using System.Net.Http;
using BankingSystem.IntegrationTests.Environment.Server;
using BankingSystem.Messages;
using BankingSystem.NotificationService.Handlers;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace BankingSystem.IntegrationTests.NotificationsTests
{
    /// <summary>
    ///     Represents an abstract base class for notification integration tests.
    /// </summary>
    public abstract class NotificationTestsBase
    {
        /// <summary>
        ///     Gets the test SignalR server.
        /// </summary>
        /// <value>
        ///     The test signal r server.
        /// </value>
        protected ISignalRServer TestSignalRServer { get; private set; }

        /// <summary>
        ///     Gets the web API server.
        /// </summary>
        /// <value>
        ///     The web API server.
        /// </value>
        protected IWebApiServer WebApiServer { get; private set; }

        /// <summary>
        ///     Gets the notification server.
        /// </summary>
        /// <value>
        ///     The notification server.
        /// </value>
        protected INotificationServer NotificationServer { get; private set; }

        /// <summary>
        ///     Gets the HTTP client.
        /// </summary>
        /// <value>
        ///     The client.
        /// </value>
        protected HttpClient HttpClient { get; private set; }

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            // SignalR
            TestSignalRServer = new TestSignalRServer();
            TestSignalRServer.Start();

            // Web API
            WebApiServer = new TestWebApiServer();
            ConfigureWebApiContainer(WebApiServer.Container);
            WebApiServer.Start();
            HttpClient = new HttpClient(WebApiServer.ServerHandler) {BaseAddress = WebApiServer.BaseAddress};

            // Notifications
            NotificationServer = new NotificationTestServer();
            ConfigureNotificationContainer(NotificationServer.Container);
            NotificationServer.Start();
        }

        [TestFixtureTearDown]
        public void FixtureTeardown()
        {
            WebApiServer.Stop();
            NotificationServer.Stop();
            TestSignalRServer.Stop();

            HttpClient.Dispose();
        }

        /// <summary>
        ///     Allows to configure notification server's container in a subclass.
        /// </summary>
        /// <param name="container">The container.</param>
        protected virtual void ConfigureNotificationContainer(IUnityContainer container)
        {
            // override the hander to synchronize flows in tests
            var handler = container.Resolve<IHandler<BalanceChangedMessage>>();
            container.RegisterType<IHandler<BalanceChangedMessage>, SyncBalanceChangedMessageHandler>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory((c => new SyncBalanceChangedMessageHandler(handler))));
        }

        /// <summary>
        ///     Allows to configure webapi server container's in a subclass.
        /// </summary>
        /// <param name="container">The container.</param>
        protected virtual void ConfigureWebApiContainer(IUnityContainer container)
        {
        }
    }
}