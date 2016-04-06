﻿using System;
using System.Net;
using System.Net.Mail;
using BankingSystem.Common.Data;
using BankingSystem.DataTier;
using BankingSystem.DataTier.Entities;

namespace BankingSystem.LogicTier.Impl
{
    /// <summary>
    ///     Represents an email service.
    /// </summary>
    /// <seealso cref="BankingSystem.LogicTier.IEmailService" />
    public class EmailService : IEmailService
    {
        private readonly IDatabaseContext _databaseContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EmailService" /> class.
        /// </summary>
        /// <param name="databaseContext">The database context.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public EmailService(IDatabaseContext databaseContext)
        {
            if (databaseContext == null)
                throw new ArgumentNullException(nameof(databaseContext));

            _databaseContext = databaseContext;
        }

        /// <summary>
        ///     Schedules the email.
        /// </summary>
        /// <param name="recipient">The recipient.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <returns>
        ///     A scheduled email.
        /// </returns>
        public IScheduledEmail ScheduleEmail(string recipient, string subject, string body)
        {
            var scheduledEmail = new ScheduledEmail {RecipientAddress = recipient, ScheduledDateTime = DateTime.UtcNow, Subject = subject, Body = body};
            _databaseContext.ScheduledEmails.Insert(scheduledEmail);
            return scheduledEmail;
        }

        /// <summary>
        ///     Delivers the scheduled emails using the given smtp settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void DeliverEmails(SmtpSettings settings)
        {
            using (var client = new SmtpClient(settings.SmtpHost, settings.SmtpPort))
            {
                client.EnableSsl = settings.EnableSsl;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(settings.SmtpUser, settings.SmtpPassword);

                foreach (var email in _databaseContext.ScheduledEmails.GetAll())
                {
                    bool delivered;

                    try
                    {
                        using (var message = new MailMessage(new MailAddress(settings.FromAddress), new MailAddress(email.RecipientAddress)))
                        {
                            message.Subject = email.Subject;
                            message.Body = email.Body;
                            message.IsBodyHtml = false;
                            client.Send(message);
                            delivered = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        email.FailureReason = ex.ToString();
                        _databaseContext.ScheduledEmails.Update(email);
                        delivered = false;
                    }

                    if (delivered)
                    {
                        EmailDelivered(email);
                    }
                }
            }
        }

        /// <summary>
        ///     Changes the state of the given email to delivered.
        /// </summary>
        /// <param name="email">The email.</param>
        private void EmailDelivered(IScheduledEmail email)
        {
            // delete the email from scheduled store and move it to delivered store.
            var deliveredEmail = new DeliveredEmail {RecipientAddress = email.RecipientAddress, DeliveredDateTime = DateTime.UtcNow, Subject = email.Subject, Body = email.Body};
            using (var transaction = _databaseContext.DemandTransaction())
            {
                _databaseContext.ScheduledEmails.Delete(email);
                _databaseContext.DeliveredEmails.Insert(deliveredEmail);
                transaction.Commit();
            }
        }
    }
}