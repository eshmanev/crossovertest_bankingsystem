namespace BankingSystem.LogicTier.Utils
{
    /// <summary>
    ///     Defines an SMTP factory.
    /// </summary>
    public interface ISmtpFactory
    {
        /// <summary>
        ///     Creates the SMTP client.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        ISmtpClient CreateClient(SmtpSettings settings);
    }
}