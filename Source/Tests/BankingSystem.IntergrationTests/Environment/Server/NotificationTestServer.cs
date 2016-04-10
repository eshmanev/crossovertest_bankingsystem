using BankingSystem.LogicTier;
using BankingSystem.NotificationService;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Topshelf;
using Topshelf.Builders;
using Topshelf.HostConfigurators;
using Topshelf.Hosts;
using Topshelf.Runtime;
using Topshelf.Runtime.Windows;

namespace BankingSystem.IntegrationTests.Environment.Server
{
    /// <summary>
    ///     Represents a server which hosts notification windows service in test environment.
    /// </summary>
    public class NotificationTestServer : INotificationServer
    {
        private ProgramControl _serviceControl;
        private TestHost _serviceHost;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NotificationTestServer" /> class.
        /// </summary>
        public NotificationTestServer()
        {
            // build the service container
            Container = BankingSystem.NotificationService.Program.BuildContainer();
            Container.RegisterType<ISettings, TestNotificationSettings>();
        }

        /// <summary>
        ///     Gets the unity container.
        /// </summary>
        /// <value>
        ///     The unity container.
        /// </value>
        public IUnityContainer Container { get; }

        /// <summary>
        ///     Starts the server.
        /// </summary>
        public void Start()
        {
            // build the service host
            _serviceControl = Container.Resolve<ProgramControl>();
            var builder = new TestBuilder(new WindowsHostEnvironment(new HostConfiguratorImpl()), new WindowsHostSettings());
            _serviceHost = (TestHost) builder.Build(new TestServiceBuilder(_serviceControl));

            // run the host
            _serviceHost.Run();
        }

        /// <summary>
        ///     Stops the server.
        /// </summary>
        public void Stop()
        {
            _serviceControl.Stop(_serviceHost);
        }

        private class TestNotificationSettings : ISettings
        {
            public string HubUrl => TestVars.SignalRBaseUrl;

            public SmtpSettings SmtpSettings => new SmtpSettings();
        }

        private class TestServiceBuilder : ServiceBuilder
        {
            private readonly ServiceControl[] _serviceControl;

            public TestServiceBuilder(params ServiceControl[] serviceControl)
            {
                _serviceControl = serviceControl;
            }

            public ServiceHandle Build(HostSettings settings)
            {
                return new TestHandle(_serviceControl);
            }
        }

        private class TestHandle : ServiceHandle
        {
            private readonly ServiceControl[] _serviceControl;

            public TestHandle(params ServiceControl[] serviceControl)
            {
                _serviceControl = serviceControl;
            }

            public void Dispose()
            {
            }

            public bool Start(HostControl hostControl)
            {
                _serviceControl.ForEach(x => x.Start(hostControl));
                return true;
            }

            public bool Pause(HostControl hostControl)
            {
                return false;
            }

            public bool Continue(HostControl hostControl)
            {
                return false;
            }

            public bool Stop(HostControl hostControl)
            {
                return false;
            }

            public void Shutdown(HostControl hostControl)
            {
            }

            public void SessionChanged(HostControl hostControl, SessionChangedArguments arguments)
            {
            }

            public void CustomCommand(HostControl hostControl, int command)
            {
            }
        }
    }
}