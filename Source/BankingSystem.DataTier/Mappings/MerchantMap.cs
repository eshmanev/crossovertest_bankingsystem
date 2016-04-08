using BankingSystem.DataTier.Entities;
using FluentNHibernate.Mapping;

namespace BankingSystem.DataTier.Mappings
{
    /// <summary>
    ///     Defines a mapping for the <see cref="Merchant" />
    /// </summary>
    public class MerchantMap : SubclassMap<Merchant>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MerchantMap" /> class.
        /// </summary>
        public MerchantMap()
        {
            DiscriminatorValue("merchant");
            Map(x => x.MerchantId);
            Map(x => x.MerchantName);
            Map(x => x.ContactPerson);
        }
    }
}