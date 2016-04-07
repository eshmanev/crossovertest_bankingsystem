using System.Configuration;

namespace BankingSystem.ATM
{
    /// <summary>
    ///     Provides application settings.
    /// </summary>
    public class Settings : ISettings
    {
        /// <summary>
        ///     Gets the web API host.
        /// </summary>
        /// <value>
        ///     The web API host.
        /// </value>
        public string WebApiHost => ConfigurationManager.AppSettings["WebApiHost"];
    }
}