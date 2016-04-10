using System;
using System.Net.Http;

namespace BankingSystem.IntergrationTests.Server
{
    /// <summary>
    ///     Represents a server which allows to run tests against a real Web API server.
    /// </summary>
    /// <seealso cref="BankingSystem.IntergrationTests.Server.IWebApiServer" />
    public class RealWebApiServer : IWebApiServer
    {
        private readonly Uri _baseAddress;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RealWebApiServer" /> class.
        /// </summary>
        /// <param name="baseAddress">The real server URL address..</param>
        public RealWebApiServer(Uri baseAddress)
        {
            _baseAddress = baseAddress;
        }

        /// <summary>
        ///     Gets the base address.
        /// </summary>
        /// <value>
        ///     The base address.
        /// </value>
        public Uri BaseAddress
        {
            get { return _baseAddress; }
        }

        /// <summary>
        ///     Gets the server handler.
        /// </summary>
        /// <value>
        ///     The server handler.
        /// </value>
        public HttpMessageHandler ServerHandler => new HttpClientHandler();

        void IWebApiServer.Start()
        {
        }

        void IWebApiServer.Stop()
        {
        }
    }
}