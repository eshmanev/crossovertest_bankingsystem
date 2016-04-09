using BankingSystem.Domain;

namespace BankingSystem.LogicTier
{
    /// <summary>
    ///     Defines an email service.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        ///     Schedules the email.
        /// </summary>
        /// <param name="recipient">The recipient.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <returns>A scheduled email.</returns>
        IScheduledEmail ScheduleEmail(string recipient, string subject, string body);

        /// <summary>
        ///     Delivers the scheduled emails using the given smtp settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        void DeliverEmails(SmtpSettings settings);
    }
}