using System;
using BankingSystem.Common.Data;

namespace BankingSystem.DataTier.Entities
{
    /// <summary>
    ///     Provides login information.
    /// </summary>
    /// <seealso cref="ILoginInfo" />
    public class LoginInfo : ILoginInfo
    {
        protected LoginInfo()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoginInfo" /> class.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="loginKey">The login key.</param>
        public LoginInfo(string providerName, string loginKey)
        {
            if (string.IsNullOrEmpty(providerName))
                throw new ArgumentNullException(nameof(providerName));

            if (string.IsNullOrEmpty(loginKey))
                throw new ArgumentNullException(nameof(loginKey));

            ProviderName = providerName;
            LoginKey = loginKey;
        }

        /// <summary>
        ///     Gets the customer.
        /// </summary>
        /// <value>
        ///     The customer.
        /// </value>
        public virtual ICustomer Customer { get; protected internal set; }

        /// <summary>
        ///     Gets the name of the login provider.
        /// </summary>
        /// <value>
        ///     The name of the login provider.
        /// </value>
        public virtual string ProviderName { get; protected set; }

        /// <summary>
        ///     Gets the login key.
        /// </summary>
        /// <value>
        ///     The login key.
        /// </value>
        public virtual string LoginKey { get; protected set; }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var other = obj as LoginInfo;
            return other != null && other.ProviderName == ProviderName && other.LoginKey == LoginKey;
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            // ReSharper disable NonReadonlyMemberInGetHashCode
            return ProviderName.GetHashCode() ^ LoginKey.GetHashCode();
            // ReSharper restore NonReadonlyMemberInGetHashCode
        }
    }
}