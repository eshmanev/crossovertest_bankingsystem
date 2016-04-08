using System;
using BankingSystem.Common.Data;
using BankingSystem.DataTier;

namespace BankingSystem.LogicTier.Impl
{
    /// <summary>
    ///     Represents a merchants service.
    /// </summary>
    /// <seealso cref="BankingSystem.LogicTier.IMerchantService" />
    public class MerchantService : IMerchantService
    {
        private readonly IDatabaseContext _databaseContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MerchantService" /> class.
        /// </summary>
        /// <param name="databaseContext">The database context.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public MerchantService(IDatabaseContext databaseContext)
        {
            if (databaseContext == null)
                throw new ArgumentNullException(nameof(databaseContext));

            _databaseContext = databaseContext;
        }

        /// <summary>
        ///     Searches for a merchant by id.
        /// </summary>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <returns>A merchant or null.</returns>
        public IMerchant FindMerchant(Guid merchantId)
        {
            return _databaseContext.Customers.FindByMerchantId(merchantId);
        }
    }
}