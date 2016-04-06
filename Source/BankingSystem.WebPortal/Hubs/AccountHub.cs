using BankingSystem.Common.Messages;
using Microsoft.AspNet.SignalR;

namespace BankingSystem.WebPortal.Hubs
{
    /// <summary>
    ///     Represents a hub of accounts.
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.SignalR.Hub" />
    public class AccountHub : Hub
    {
        /// <summary>
        ///     Delivers notification when the account's balance is changed.
        /// </summary>
        /// <param name="message">The message.</param>
        public void OnBalanceChanged(BalanceChangedMessage message)
        {
            Clients.All.onBalanceChanged(message);
        }
    }
}