using BankingSystem.Domain;

namespace BankingSystem.DataTier.Repositories
{
    /// <summary>
    ///     Defines a repository of accounts.
    /// </summary>
    public interface IAccountRepository : IRepository<IAccount>
    {
        /// <summary>
        ///     Searches for account by its number.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns>
        ///     An account or null.
        /// </returns>
        IAccount FindAccount(string accountNumber);
    }
}