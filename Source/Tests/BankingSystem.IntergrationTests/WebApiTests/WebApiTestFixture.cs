using System.Net.Http;
using BankingSystem.IntergrationTests.Server;
using NUnit.Framework;

namespace BankingSystem.IntergrationTests.WebApiTests
{
    /// <summary>
    ///     Defines a base class for Web API integration tests.
    /// </summary>
    public abstract class WebApiTestFixture
    {
        private IWebApiServer _server;

        /// <summary>
        ///     Gets the HTTP client.
        /// </summary>
        /// <value>
        ///     The client.
        /// </value>
        protected HttpClient Client { get; private set; }

        [TestFixtureSetUp]
        public virtual void FixtureSetup()
        {
            _server = new WebApiTestServer();
            _server.Start();
            Client = new HttpClient(_server.ServerHandler) {BaseAddress = _server.BaseAddress};
        }

        [TestFixtureTearDown]
        public virtual void FixtureTeardown()
        {
            _server.Stop();
            Client.Dispose();
        }
    }
}