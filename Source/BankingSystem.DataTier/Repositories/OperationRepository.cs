using BankingSystem.Common.Data;
using BankingSystem.DataTier.Session;

namespace BankingSystem.DataTier.Repositories
{
    /// <summary>
    ///     Represents a repository of operations.
    /// </summary>
    public class OperationRepository : Repository<IOperation>, IOperationRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OperationRepository" /> class.
        /// </summary>
        /// <param name="databaseContext"></param>
        public OperationRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}