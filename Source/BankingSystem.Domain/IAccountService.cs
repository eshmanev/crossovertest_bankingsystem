using BankingSystem.Data;

namespace BankingSystem.Domain
{
    /// <summary>
    /// Defines a service of accounts.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Transfers money from the source account to the destination account.
        /// </summary>
        /// <param name="sourceAccount">The source account.</param>
        /// <param name="destAccount">The dest account.</param>
        /// <param name="amount">The amount to transfer.</param>
        void TransferMoney(IAccount sourceAccount, IAccount destAccount, decimal amount);
    }
}