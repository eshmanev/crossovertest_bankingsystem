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
        private IDisposable _host;

        /// <summary>
        ///     Starts the server.
        /// </summary>
        public void Start()
        {
            _host = WebApp.Start<Startup>(TestVars.SignalRBaseUrl);
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