﻿using System.Threading.Tasks;
using BankingSystem.Common.Data;
using BankingSystem.LogicTier.Impl;

namespace BankingSystem.LogicTier
{
    /// <summary>
    ///     Defines a service of accounts.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        ///     Transfers money from the source account to the destination account.
        /// </summary>
        /// <param name="sourceAccount">The source account.</param>
        /// <param name="destAccount">The dest account.</param>
        /// <param name="amount">The amount to transfer.</param>
        Task TransferMoney(IAccount sourceAccount, IAccount destAccount, decimal amount);

        /// <summary>
        ///     Updates the balance with the specified amount.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="changeAmount">The change amount.</param>
        /// <exception cref="BankingServiceException">Account balance exeeds limits.</exception>
        void UpdateBalance(IAccount account, decimal changeAmount);

        /// <summary>
        ///     Searches for account by its number.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns>An account or null.</returns>
        IAccount FindAccount(string accountNumber);
    }
}