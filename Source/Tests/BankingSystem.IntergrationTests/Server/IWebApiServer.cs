using System;
using System.Net.Http;

namespace BankingSystem.IntergrationTests.Server
{
    /// <summary>
    ///     Defines a web api server.
    /// </summary>
    public interface IWebApiServer
    {
        /// <summary>
        ///     Gets the base address.
        /// </summary>
        /// <value>
        ///     The base address.
        /// </value>
        Uri BaseAddress { get; }

        /// <summary>
        ///     Gets the server handler.
        /// </summary>
        /// <value>
        ///     The server handler.
        /// </value>
        HttpMessageHandler ServerHandler { get; }

        /// <summary>
        ///     Starts the server.
        /// </summary>
        void Start();

        /// <summary>
        ///     Stops the server.
        /// </summary>
        void Stop();
    }
}