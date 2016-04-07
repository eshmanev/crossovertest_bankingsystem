using System;
using System.Net.Mail;

namespace BankingSystem.LogicTier.Utils
{
    /// <summary>
    ///     Defines an SMTP client;
    /// </summary>
    public interface ISmtpClient : IDisposable
    {
        /// <summary>
        ///     Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Send(MailMessage message);
    }
}