using BankingSystem.Common.Data;
using BankingSystem.DataTier.Session;

namespace BankingSystem.DataTier.Repositories
{
    /// <summary>
    ///     Represents a repository of delivered emails.
    /// </summary>
    public class DeliveredEmailRepository : Repository<IDeliveredEmail>, IDeliveredEmailRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DeliveredEmailRepository" /> class.
        /// </summary>
        /// <param name="databaseContext"></param>
        public DeliveredEmailRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}