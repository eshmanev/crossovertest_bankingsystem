namespace BankingSystem.Common.Data
{
    /// <summary>
    /// Defines a login information used for logging in with social accounts.
    /// </summary>
    public interface ILoginInfo
    {
        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <value>
        /// The customer.
        /// </value>
        ICustomer Customer { get; }

        /// <summary>
        /// Gets the name of the login provider.
        /// </summary>
        /// <value>
        /// The name of the login provider.
        /// </value>
        string ProviderName { get; }

        /// <summary>
        /// Gets the login key.
        /// </summary>
        /// <value>
        /// The login key.
        /// </value>
        string LoginKey { get; }
    }
}