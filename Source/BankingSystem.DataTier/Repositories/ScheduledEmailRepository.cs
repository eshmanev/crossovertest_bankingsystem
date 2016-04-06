using BankingSystem.Common.Data;
using BankingSystem.DataTier.Session;

namespace BankingSystem.DataTier.Repositories
{
    /// <summary>
    ///     Represents a repository of scheduled emails.
    /// </summary>
    public class ScheduledEmailRepository : Repository<IScheduledEmail>, IScheduledEmailRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ScheduledEmailRepository" /> class.
        /// </summary>
        /// <param name="databaseContext"></param>
        public ScheduledEmailRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}