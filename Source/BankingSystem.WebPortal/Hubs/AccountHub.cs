using BankingSystem.Data;
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
        ///     Delivers notification when the given account is changed.
        /// </summary>
        /// <param name="account">The account.</param>
        public void OnAccountChanged(IAccount account)
        {
            Clients.All.onAccountChanged(account);
        }
    }
}