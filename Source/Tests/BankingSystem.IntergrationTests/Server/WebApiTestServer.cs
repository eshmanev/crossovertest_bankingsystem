﻿using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using BankingSystem.WebPortal;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace BankingSystem.IntergrationTests.Server
{
    /// <summary>
    ///     Represents a server which hosts Web API in test environment.
    /// </summary>
    public class WebApiTestServer : IWebApiServer
    {
        private HttpServer _server;

        /// <summary>
        ///     Gets the base address.
        /// </summary>
        /// <value>
        ///     The base address.
        /// </value>
        public Uri BaseAddress => new Uri("http://localhost:1234");

        /// <summary>
        ///     Gets the server handler.
        /// </summary>
        /// <value>
        ///     The server handler.
        /// </value>
        public HttpMessageHandler ServerHandler => _server;

        /// <summary>
        ///     Starts the server.
        /// </summary>
        public void Start()
        {
            try
            {
                // configure container
                UnityConfig.SetPerRequestManagerFactory(() => new ContainerControlledLifetimeManager());
                UnityConfig.ConfigureContainer();

                // configure HTTP
                var httpConfig = new HttpConfiguration();
                httpConfig.Services.Replace(typeof (IAssembliesResolver), new TestAssembliesResolver());
                httpConfig.MapHttpAttributeRoutes();
                httpConfig.DependencyResolver = new Microsoft.Practices.Unity.WebApi.UnityDependencyResolver(UnityConfig.Container);

                // create the server
                _server = new HttpServer(httpConfig);
            }
            catch (Exception e)
            {
                Assert.Fail("Could not create server: {0}", e);
            }
        }

        /// <summary>
        ///     Stops the server.
        /// </summary>
        public void Stop()
        {
            try
            {
                _server.Dispose();
            }
            catch (Exception e)
            {
                Assert.Fail("Could not stop server: {0}", e);
            }
        }
    }
}