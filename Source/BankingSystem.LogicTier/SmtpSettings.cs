namespace BankingSystem.LogicTier
{
    /// <summary>
    ///     Provides SMTP settings.
    /// </summary>
    public struct SmtpSettings
    {
        /// <summary>
        ///     Gets the SMTP host.
        /// </summary>
        /// <value>
        ///     The SMTP host.
        /// </value>
        public string SmtpHost { get; set; }

        /// <summary>
        ///     Gets the SMTP port.
        /// </summary>
        /// <value>
        ///     The SMTP port.
        /// </value>
        public int SmtpPort { get; set; }

        /// <summary>
        ///     Gets the SMTP user.
        /// </summary>
        /// <value>
        ///     The SMTP user.
        /// </value>
        public string SmtpUser { get; set; }

        /// <summary>
        ///     Gets the SMTP password.
        /// </summary>
        /// <value>
        ///     The SMTP password.
        /// </value>
        public string SmtpPassword { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether SSL is enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if SSL is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool EnableSsl { get; set; }

        /// <summary>
        ///     Gets or sets from address.
        /// </summary>
        /// <value>
        ///     From address.
        /// </value>
        public string FromAddress { get; set; }
    }
}