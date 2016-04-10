using System;
using Microsoft.Owin.Hosting;
using Owin;

namespace BankingSystem.IntegrationTests.Environment.Server
{
    /// <summary>
    ///     Represents a test SignalR server host.
    /// </summary>
    public class TestSignalRServer : ISignalRServer
    {
        private readonly int _portNumber;
        private IDisposable _host;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TestSignalRServer" /> class.
        /// </summary>
        /// <param name="portNumber">The port number.</param>
        public TestSignalRServer(int portNumber)
        {
            _portNumber = portNumber;
        }

        /// <summary>
        ///     Starts the server.
        /// </summary>
        public void Start()
        {
            _host = WebApp.Start<Startup>("http://localhost:" + _portNumber);
        }

        /// <summary>
        ///     Stops the server.
        /// </summary>
        public void Stop()
        {
            _host.Dispose();
        }

        private class Startup
        {
            public void Configuration(IAppBuilder app)
            {
                app.MapSignalR();
            }
        }
    }
}