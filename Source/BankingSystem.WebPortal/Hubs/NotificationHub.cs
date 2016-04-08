using BankingSystem.Common.Messages;
using Microsoft.AspNet.SignalR;

namespace BankingSystem.WebPortal.Hubs
{
    /// <summary>
    ///     Represents a hub of notifications.
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.SignalR.Hub" />
    // [Authorize]
    public class NotificationHub : Hub
    {
        /// <summary>
        ///     Delivers notification when the account's balance is changed.
        /// </summary>
        /// <param name="message">The message.</param>
        public void OnBalanceChanged(BalanceChangedMessage message)
        {
            Clients.All.onBalanceChanged(message);
        }

        /// <summary>
        ///     Called when a new journal created.
        /// </summary>
        /// <param name="message">The message.</param>
        public void OnJournalCreated(JournalCreatedMessage message)
        {
            Clients.All.onJournalCreated(message);
        }
    }
}