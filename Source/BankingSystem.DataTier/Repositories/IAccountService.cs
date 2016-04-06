using BankingSystem.Common.Data;

namespace BankingSystem.DataTier.Repositories
{
    /// <summary>
    ///     Defines a repository of logins.
    /// </summary>
    public interface ILoginInfoRepository : IRepository<ILoginInfo>
    {
        /// <summary>
        ///     Searches for a customer by social account.
        /// </summary>
        /// <param name="providerName">Name of the login provider.</param>
        /// <param name="loginKey">The login key.</param>
        /// <returns>
        ///     An instance of the <see cref="ICustomer" /> or null.
        /// </returns>
        ILoginInfo FindByProviderAndLogin(string providerName, string loginKey);
    }
}