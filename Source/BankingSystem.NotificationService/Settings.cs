using System;
using System.Configuration;
using BankingSystem.LogicTier;

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

        /// <summary>
        ///     Gets the SMTP settings.
        /// </summary>
        /// <value>
        ///     The SMTP settings.
        /// </value>
        public SmtpSettings SmtpSettings => new SmtpSettings
        {
            SmtpHost = ConfigurationManager.AppSettings["SmtpHost"],
            SmtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]),
            EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["SmtpSSL"]),
            SmtpUser = ConfigurationManager.AppSettings["SmtpUser"],
            SmtpPassword = ConfigurationManager.AppSettings["SmtpPassword"],
            FromAddress = ConfigurationManager.AppSettings["SmtpFromAddress"],
        };
    }
}