using System;
using System.Threading.Tasks;
using BankingSystem.Common.Data;
using BankingSystem.Common.Messages;
using BankingSystem.LogicTier;
using Microsoft.AspNet.SignalR.Hubs;

namespace BankingSystem.WebPortal.Services
{
    /// <summary>
    ///     Represents an account service decorated with automatic notifications of account balance changes.
    /// </summary>
    /// <seealso cref="BankingSystem.LogicTier.IAccountService" />
    public class AccountServiceWithDecorator : IAccountService
    {
        private readonly IAccountService _original;
        private readonly IHubConnectionContext<dynamic> _hubContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountServiceWithDecorator" /> class.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="hubContext">The hub context.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public AccountServiceWithDecorator(IAccountService original, IHubConnectionContext<dynamic> hubContext)
        {
            if (original == null)
                throw new ArgumentNullException(nameof(original));
            if (hubContext == null)
                throw new ArgumentNullException(nameof(hubContext));

            _original = original;
            _hubContext = hubContext;
        }

        /// <summary>
        ///     Transfers money from the source account to the destination account.
        /// </summary>
        /// <param name="sourceAccount">The source account.</param>
        /// <param name="destAccount">The dest account.</param>
        /// <param name="amount">The amount to transfer.</param>
        /// <param name="mode">The conversion mode.</param>
        /// <param name="description">The description of the transaction.</param>
        /// <returns>
        ///     The operation.
        /// </returns>
        public async Task TransferMoney(IAccount sourceAccount, IAccount destAccount, decimal amount, AmountConversionMode mode, string description)
        {
            var oldSourceBalance = sourceAccount.Balance;
            var oldDestBalance = destAccount.Balance;
            await _original.TransferMoney(sourceAccount, destAccount, amount, mode, description);
            _hubContext.All.onBalanceChanged(BalanceChangedMessage.Create(sourceAccount, oldSourceBalance, description));
            _hubContext.All.onBalanceChanged(BalanceChangedMessage.Create(destAccount, oldDestBalance, description));
        }

        /// <summary>
        ///     Updates the balance with the specified amount.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="changeAmount">The change amount. Can be negative and positive.</param>
        /// <param name="description">The description of the transaction.</param>
        public void UpdateBalance(IAccount account, decimal changeAmount, string description)
        {
            var oldBalance = account.Balance;
            _original.UpdateBalance(account, changeAmount, description);
            _hubContext.All.onBalanceChanged(BalanceChangedMessage.Create(account, oldBalance, description));
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