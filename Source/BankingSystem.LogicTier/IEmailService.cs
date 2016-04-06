﻿using BankingSystem.Common.Data;

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
        ///     Changes the state of the given email to delivered.
        /// </summary>
        /// <param name="email">The email.</param>
        void EmailDelivered(IScheduledEmail email);
    }
}