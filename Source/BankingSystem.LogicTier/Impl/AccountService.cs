using System;
using System.Threading.Tasks;
using BankingSystem.Common.Data;
using BankingSystem.DataTier;
using BankingSystem.DataTier.Entities;

namespace BankingSystem.LogicTier.Impl
{
    /// <summary>
    ///     Represents a service of accounts.
    /// </summary>
    /// <seealso cref="IAccountService" />
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
        public async Task TransferMoney(IAccount sourceAccount, IAccount destAccount, decimal amount)
        {
            if (sourceAccount == null)
                throw new ArgumentNullException(nameof(sourceAccount));

            if (destAccount == null)
                throw new ArgumentNullException(nameof(destAccount));

            if (amount <= 0)
                throw new ArgumentException("Amount should be greater than 0", nameof(amount));

            if (sourceAccount.Balance < amount)
                throw new BankingServiceException("The account does not have enough amount of money");

            using (var transaction = _databaseContext.DemandTransaction())
            {
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
                        exhangeRate = await _exchangeRateService.GetExhangeRateAsync(sourceAccount.Currency, destAccount.Currency);
                        commissionPercent = DefaultCommission;
                    }

                    // create a new operation entry
                    var commission = Math.Round(amount*commissionPercent, 2);
                    var operation = new Operation(
                        DateTime.UtcNow,
                        amount,
                        commission,
                        $"Money transfer from the account {sourceAccount.AccountNumber} to the account {destAccount.AccountNumber}");
                    _databaseContext.Operations.Insert(operation);

                    // update source account
                    sourceAccount.Balance = Math.Round(sourceAccount.Balance - amount, 2);
                    _databaseContext.Accounts.Update(sourceAccount);

                    // update dest account
                    destAccount.Balance = Math.Round(destAccount.Balance + exhangeRate*(amount - commission), 2);
                    _databaseContext.Accounts.Update(destAccount);

                    // update revenues
                    if (commission > 0)
                    {
                        _bankBalanceService.AddRevenue(commission, $"A commission for money transfer from the account {sourceAccount.AccountNumber} to the account {destAccount.AccountNumber}");
                    }

                    // commit transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    var message = $"An error has occurred while transferring money from the account {sourceAccount.AccountNumber} to the account {destAccount.AccountNumber}";
                    throw new BankingServiceException(message, ex);
                }
            }
        }

        /// <summary>
        ///     Updates the balance with the specified amount.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="changeAmount">The change amount.</param>
        /// <exception cref="BankingServiceException">Account balance exeeds limits.</exception>
        public void UpdateBalance(IAccount account, decimal changeAmount)
        {
            var newBalance = account.Balance + changeAmount;
            if (newBalance < 0)
                throw new BankingServiceException("Account balance exeeds limits.");

            account.Balance = newBalance;
            _databaseContext.Accounts.Update(account);
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
            return _databaseContext.Accounts.FindAccount(accountNumber);
        }
    }
}