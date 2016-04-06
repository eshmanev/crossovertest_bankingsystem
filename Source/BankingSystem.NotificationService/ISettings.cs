namespace BankingSystem.NotificationService
{
    /// <summary>
    ///     Defines application settings.
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        ///     Gets the hub URL.
        /// </summary>
        /// <value>
        ///     The hub URL.
        /// </value>
        string HubUrl { get; }
    }
}