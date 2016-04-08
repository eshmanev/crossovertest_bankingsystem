using System;
using System.Threading.Tasks;
using BankingSystem.Common.Data;
using BankingSystem.Common.Messages;
using BankingSystem.LogicTier;
using BankingSystem.WebPortal.Hubs;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Practices.Unity;

namespace BankingSystem.WebPortal.Services
{
    /// <summary>
    ///     Represents an account service decorated with automatic notifications of account balance changes.
    /// </summary>
    /// <seealso cref="BankingSystem.LogicTier.IAccountService" />
    public class AccountServiceWithNotificationsDecorator : IAccountService
    {
        private readonly IAccountService _original;
        private readonly IHubConnectionContext<dynamic> _hubContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountServiceWithNotificationsDecorator" /> class.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="hubContext">The hub context.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public AccountServiceWithNotificationsDecorator(IAccountService original, [Dependency(HubNames.AccountHub)] IHubConnectionContext<dynamic> hubContext)
        {
            if (original == null)
                throw new ArgumentNullException(nameof(original));
            if (hubContext == null) throw new ArgumentNullException(nameof(hubContext));

            _original = original;
            _hubContext = hubContext;
        }

        /// <summary>
        ///     Transfers money from the source account to the destination account.
        /// </summary>
        /// <param name="sourceAccount">The source account.</param>
        /// <param name="destAccount">The dest account.</param>
        /// <param name="amount">The amount to transfer.</param>
        /// <returns></returns>
        public async Task TransferMoney(IAccount sourceAccount, IAccount destAccount, decimal amount)
        {
            var oldSourceBalance = sourceAccount.Balance;
            var oldDestBalance = destAccount.Balance;
            await _original.TransferMoney(sourceAccount, destAccount, amount);
            _hubContext.All.onBalanceChanged(BalanceChangedMessage.Create(sourceAccount, oldSourceBalance));
            _hubContext.All.onBalanceChanged(BalanceChangedMessage.Create(destAccount, oldDestBalance));
        }

        /// <summary>
        ///     Updates the balance with the specified amount.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="changeAmount">The change amount. Can be negative and positive.</param>
        public void UpdateBalance(IAccount account, decimal changeAmount)
        {
            var oldBalance = account.Balance;
            _original.UpdateBalance(account, changeAmount);
            _hubContext.All.onBalanceChanged(BalanceChangedMessage.Create(account, oldBalance));
        }

        /// <summary>
        ///     Searches for account by its number.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns>
        ///     An account or null.
        /// </returns>
        public IAccount FindAccount(string accountNumber)
        {
            return _original.FindAccount(accountNumber);
        }
    }
}