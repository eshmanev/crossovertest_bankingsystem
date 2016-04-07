using System.Linq;
using BankingSystem.Common.Data;

namespace BankingSystem.DataTier.Repositories.Impl
{
    /// <summary>
    ///     Represents a repository of accounts.
    /// </summary>
    public class AccountRepository : Repository<IAccount>, IAccountRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountRepository" /> class.
        /// </summary>
        /// <param name="databaseContext"></param>
        public AccountRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
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
            return base.Filter(x => x.AccountNumber == accountNumber).FirstOrDefault();
        }
    }
}