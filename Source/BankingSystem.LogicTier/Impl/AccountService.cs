using System;
using System.Linq;
using System.Threading.Tasks;
using BankingSystem.Common.Data;
using BankingSystem.DataTier;
using Microsoft.Practices.ObjectBuilder2;

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
        private readonly IJournalService _journalService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountService" /> class.
        /// </summary>
        /// <param name="databaseContext">The database context.</param>
        /// <param name="exchangeRateService">The exchange rate service.</param>
        /// <param name="bankBalanceService">The bank balance service.</param>
        /// <param name="journalService">The journal service.</param>
        public AccountService(IDatabaseContext databaseContext, IExchangeRateService exchangeRateService, IBankBalanceService bankBalanceService, IJournalService journalService)
        {
            if (databaseContext == null)
                throw new ArgumentNullException(nameof(databaseContext));

            if (exchangeRateService == null)
                throw new ArgumentNullException(nameof(exchangeRateService));

            if (bankBalanceService == null)
                throw new ArgumentNullException(nameof(bankBalanceService));

            if (journalService == null)
                throw new ArgumentNullException(nameof(journalService));

            _exchangeRateService = exchangeRateService;
            _databaseContext = databaseContext;
            _bankBalanceService = bankBalanceService;
            _journalService = journalService;
        }

        /// <summary>
        ///     Transfers money from the source account to the destination account.
        /// </summary>
        /// <param name="sourceAccount">The source account.</param>
        /// <param name="destAccount">The dest account.</param>
        /// <param name="amount">The amount to transfer.</param>
        /// <param name="description">The description of the transaction.</param>
        /// <exception cref="BankingServiceException">The account does not have enough amount of money.</exception>
        public async Task TransferMoney(IAccount sourceAccount, IAccount destAccount, decimal amount, string description)
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

                    // calc bank commission and new balances
                    var commission = Math.Round(amount*commissionPercent, 2);
                    sourceAccount.Balance = Math.Round(sourceAccount.Balance - amount, 2);
                    destAccount.Balance = Math.Round(destAccount.Balance + exhangeRate*(amount - commission), 2);

                    // update database
                    _databaseContext.Accounts.Update(sourceAccount);
                    _databaseContext.Accounts.Update(destAccount);

                    // update bank revenues, if any
                    if (commission > 0)
                        _bankBalanceService.AddRevenue(commission, $"A commission for money transfer from the account {sourceAccount.AccountNumber} to the account {destAccount.AccountNumber}");

                    // write journals
                    _journalService.WriteTransferJournal(sourceAccount, destAccount, description, commission);

                    // commit transaction
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        ///     Updates the balance with the specified amount.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="changeAmount">The change amount. Can be negative and positive.</param>
        /// <param name="description">The description of the transaction.</param>
        /// <exception cref="BankingServiceException">Account balance exeeds limits.</exception>
        public void UpdateBalance(IAccount account, decimal changeAmount, string description)
        {
            var newBalance = account.Balance + changeAmount;
            if (newBalance < 0)
                throw new BankingServiceException("Account balance exeeds limits.");

            using (var transaction = _databaseContext.DemandTransaction())
            {
                try
                {
                    account.Balance += changeAmount;
                    _databaseContext.Accounts.Update(account);

                    _journalService.WriteJournal(account, description);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
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