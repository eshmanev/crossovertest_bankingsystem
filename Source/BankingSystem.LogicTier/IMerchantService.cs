using System;
using BankingSystem.Common.Data;

namespace BankingSystem.LogicTier
{
    /// <summary>
    ///     Defines a merchant service.
    /// </summary>
    public interface IMerchantService
    {
        /// <summary>
        ///     Searches for a merchant by id.
        /// </summary>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <returns>A merchant or null.</returns>
        IMerchant FindMerchant(Guid merchantId);
    }
}