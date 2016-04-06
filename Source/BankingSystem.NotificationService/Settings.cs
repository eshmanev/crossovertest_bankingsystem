using System.Configuration;

namespace BankingSystem.NotificationService
{
    /// <summary>
    ///     Represents application settings.
    /// </summary>
    /// <seealso cref="BankingSystem.NotificationService.ISettings" />
    public class Settings : ISettings
    {
        /// <summary>
        ///     Gets the hub URL.
        /// </summary>
        /// <value>
        ///     The hub URL.
        /// </value>
        public string HubUrl => ConfigurationManager.AppSettings["HubUrl"];
    }
}