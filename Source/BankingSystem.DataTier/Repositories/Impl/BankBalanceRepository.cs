using BankingSystem.Domain;

namespace BankingSystem.DataTier.Repositories.Impl
{
    /// <summary>
    ///     Represents a repository of bank balance.
    /// </summary>
    public class BankBalanceRepository : Repository<IBankBalance>, IBankBalanceRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BankBalanceRepository" /> class.
        /// </summary>
        /// <param name="databaseContext"></param>
        public BankBalanceRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}