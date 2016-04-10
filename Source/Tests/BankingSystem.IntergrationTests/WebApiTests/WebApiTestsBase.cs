using System.Net.Http;
using BankingSystem.IntegrationTests.Environment.Server;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace BankingSystem.IntegrationTests.WebApiTests
{
    /// <summary>
    ///     Defines a base class for Web API integration tests.
    /// </summary>
    public abstract class WebApiTestsBase
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
        public void FixtureSetup()
        {
            _server = new TestWebApiServer();
            ConfigureWebApiContainer(_server.Container);
            _server.Start();
            Client = new HttpClient(_server.ServerHandler) {BaseAddress = _server.BaseAddress};
        }

        [TestFixtureTearDown]
        public void FixtureTeardown()
        {
            _server.Stop();
            Client.Dispose();
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