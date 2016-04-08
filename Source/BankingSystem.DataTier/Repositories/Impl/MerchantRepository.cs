using BankingSystem.Common.Data;

namespace BankingSystem.DataTier.Repositories.Impl
{
    /// <summary>
    ///     Represents a repository of merchants.
    /// </summary>
    /// <seealso cref="BankingSystem.DataTier.Repositories.IMerchantRepository" />
    public class MerchantRepository : Repository<IMerchant>, IMerchantRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MerchantRepository" /> class.
        /// </summary>
        /// <param name="databaseContext"></param>
        public MerchantRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}