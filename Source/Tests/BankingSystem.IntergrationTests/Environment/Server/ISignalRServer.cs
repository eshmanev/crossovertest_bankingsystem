namespace BankingSystem.IntegrationTests.Environment.Server
{
    /// <summary>
    ///     Defines a SignalR server.
    /// </summary>
    public interface ISignalRServer
    {
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