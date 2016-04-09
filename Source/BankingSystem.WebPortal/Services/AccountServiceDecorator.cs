using System;
using System.Threading.Tasks;
using BankingSystem.Domain;
using BankingSystem.LogicTier;
using BankingSystem.Messages;
using Microsoft.AspNet.SignalR.Hubs;

namespace BankingSystem.WebPortal.Services
{
    /// <summary>
    ///     Represents an account service decorated with automatic notifications of account balance changes.
    /// </summary>
    /// <seealso cref="BankingSystem.LogicTier.IAccountService" />
    public class AccountServiceDecorator : IAccountService
    {
        private readonly IAccountService _original;
        private readonly IHubConnectionContext<dynamic> _hubContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountServiceDecorator" /> class.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="hubContext">The hub context.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public AccountServiceDecorator(IAccountService original, IHubConnectionContext<dynamic> hubContext)
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
            _hubContext.All.onBalanceChanged(CreateMessage(sourceAccount, oldSourceBalance, description));
            _hubContext.All.onBalanceChanged(CreateMessage(destAccount, oldDestBalance, description));
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
            _hubContext.All.onBalanceChanged(CreateMessage(account, oldBalance, description));
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

        /// <summary>
        ///     Creates the balance changed message.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="oldBalance">The old balance.</param>
        /// <param name="description">The description.</param>
        /// <returns>A create instance.</returns>
        private static BalanceChangedMessage CreateMessage(IAccount account, decimal oldBalance, string description)
        {
            return new BalanceChangedMessage
            {
                AccountNumber = account.AccountNumber,
                Currency = account.Currency,
                ChangeAmount = account.Balance - oldBalance,
                CurrentBalance = account.Balance,
                Description = description
            };
        }
    }
}