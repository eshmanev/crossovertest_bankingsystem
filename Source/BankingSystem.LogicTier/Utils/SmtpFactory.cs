using System.Net;
using System.Net.Mail;

namespace BankingSystem.LogicTier.Utils
{
    /// <summary>
    ///     Represents an SMTP factory.
    /// </summary>
    /// <seealso cref="ISmtpFactory" />
    internal class SmtpFactory : ISmtpFactory
    {
        /// <summary>
        ///     Creates the SMTP client.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ISmtpClient CreateClient(SmtpSettings settings)
        {
            var client = new SmtpClient(settings.SmtpHost, settings.SmtpPort)
            {
                EnableSsl = settings.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(settings.SmtpUser, settings.SmtpPassword)
            };
            return new SmtpClientWrapper(client);
        }

        /// <summary>
        ///     Represents a wrapper of the <see cref="SmtpClient" />.
        /// </summary>
        /// <seealso cref="ISmtpClient" />
        private class SmtpClientWrapper : ISmtpClient
        {
            private readonly SmtpClient _client;

            /// <summary>
            ///     Initializes a new instance of the <see cref="SmtpClientWrapper" /> class.
            /// </summary>
            /// <param name="client">The client.</param>
            public SmtpClientWrapper(SmtpClient client)
            {
                _client = client;
            }

            /// <summary>
            ///     Sends the specified message.
            /// </summary>
            /// <param name="message">The message.</param>
            public void Send(MailMessage message)
            {
                _client.Send(message);
            }

            /// <summary>
            ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                _client.Dispose();
            }
        }
    }
}