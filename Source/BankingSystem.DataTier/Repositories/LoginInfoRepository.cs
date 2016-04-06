using System.Linq;
using BankingSystem.Common.Data;
using BankingSystem.DataTier.Session;

namespace BankingSystem.DataTier.Repositories
{
    /// <summary>
    ///     Represents a repository of logins.
    /// </summary>
    public class LoginInfoRepository : Repository<ILoginInfo>, ILoginInfoRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LoginInfoRepository" /> class.
        /// </summary>
        /// <param name="databaseContext"></param>
        public LoginInfoRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public ILoginInfo FindByProviderAndLogin(string providerName, string loginKey)
        {
            return base.Filter(x => x.ProviderName == providerName && x.LoginKey == loginKey).FirstOrDefault();
        }
    }
}