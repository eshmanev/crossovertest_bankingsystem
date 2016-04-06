using BankingSystem.LogicTier;

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

        /// <summary>
        /// Gets the SMTP settings.
        /// </summary>
        /// <value>
        /// The SMTP settings.
        /// </value>
        SmtpSettings SmtpSettings { get; }
    }
}