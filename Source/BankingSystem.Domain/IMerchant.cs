using System;

namespace BankingSystem.Domain
{
    /// <summary>
    ///     Defines a merchant
    /// </summary>
    public interface IMerchant : ICustomer
    {
        /// <summary>
        ///     Gets the merchant identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        Guid MerchantId { get; }

        /// <summary>
        ///     Gets the organization name.
        /// </summary>
        /// <value>
        ///     The organization name.
        /// </value>
        string MerchantName { get; }

        /// <summary>
        ///     Gets the contact person.
        /// </summary>
        /// <value>
        ///     The contact person.
        /// </value>
        string ContactPerson { get; }
    }
}