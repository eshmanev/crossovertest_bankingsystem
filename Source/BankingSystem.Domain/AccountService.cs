using System;
using BankingSystem.Data;
using BankingSystem.DAL;
using BankingSystem.DAL.Entities;

namespace BankingSystem.Domain
{
    /// <summary>
    ///     Represents a service of accounts.
    /// </summary>
    /// <seealso cref="BankingSystem.Domain.IAccountService" />
    public class AccountService : IAccountService
    {
        public const decimal DefaultCommission = 0.02m;
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IDatabaseContext _databaseContext;
        private readonly IBankBalanceService _bankBalanceService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountService" /> class.
        /// </summary>
        /// <param name="databaseContext">The database context.</param>
        /// <param name="exchangeRateService">The exchange rate service.</param>
        /// <param name="bankBalanceService">The bank balance service.</param>
        public AccountService(IDatabaseContext databaseContext, IExchangeRateService exchangeRateService, IBankBalanceService bankBalanceService)
        {
            if (databaseContext == null)
                throw new ArgumentNullException(nameof(databaseContext));

            if (exchangeRateService == null)
                throw new ArgumentNullException(nameof(exchangeRateService));

            if (bankBalanceService == null)
                throw new ArgumentNullException(nameof(bankBalanceService));

            _exchangeRateService = exchangeRateService;
            _databaseContext = databaseContext;
            _bankBalanceService = bankBalanceService;
        }

        /// <summary>
        ///     Transfers money from the source account to the destination account.
        /// </summary>
        /// <param name="sourceAccount">The source account.</param>
        /// <param name="destAccount">The dest account.</param>
        /// <param name="amount">The amount to transfer.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void TransferMoney(IAccount sourceAccount, IAccount destAccount, decimal amount)
        {
            if (sourceAccount == null)
                throw new ArgumentNullException(nameof(sourceAccount));

            if (destAccount == null)
                throw new ArgumentNullException(nameof(destAccount));

            if (amount <= 0)
                throw new ArgumentException("Amount should be greater than 0", nameof(amount));

            if (sourceAccount.Balance < amount)
                throw new BankingServiceException("The account does not have enough amount of money");
            
            try
            {
                // get exchange rates and commissions
                decimal exhangeRate;
                decimal commissionPercent;
                if (string.Equals(sourceAccount.Currency, destAccount.Currency, StringComparison.InvariantCultureIgnoreCase))
                {
                    exhangeRate = 1.0m;
                    commissionPercent = 0.0m;
                }
                else
                {
                    exhangeRate = _exchangeRateService.GetExhangeRate(sourceAccount.Currency, destAccount.Currency);
                    commissionPercent = DefaultCommission;
                }

                // create a new operation entry
                var commission = amount*commissionPercent;
                var operation = new Operation(
                    DateTime.UtcNow,
                    amount,
                    commission,
                    $"Money transfer from the account {sourceAccount.AccountNumber} to the account {destAccount.AccountNumber}");
                _databaseContext.Operations.Insert(operation);

                // update source account
                sourceAccount.Balance -= amount;
                _databaseContext.Accounts.Update(sourceAccount);

                // update dest account
                destAccount.Balance += exhangeRate*(amount - commission);
                _databaseContext.Accounts.Update(destAccount);

                // update revenues
                _bankBalanceService.AddRevenue(commission, $"A commission for money transfer from the account {sourceAccount.AccountNumber} to the account {destAccount.AccountNumber}");

                // commit transaction
                _databaseContext.CommitTransactionScope();
            }
            catch (Exception ex)
            {
                _databaseContext.RollbackTransactionScope();
                var message = $"An error has occurred while transferring money from the account {sourceAccount.AccountNumber} to the account {destAccount.AccountNumber}";
                throw new BankingServiceException(message, ex);
            }
        }
    }
}