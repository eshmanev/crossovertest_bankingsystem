using Microsoft.Practices.Unity;

namespace BankingSystem.IntegrationTests.Environment.Server
{
    /// <summary>
    ///     Represents a server of notification service.
    /// </summary>
    public interface INotificationServer
    {
        /// <summary>
        ///     Gets the container.
        /// </summary>
        /// <value>
        ///     The container.
        /// </value>
        IUnityContainer Container { get; }

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