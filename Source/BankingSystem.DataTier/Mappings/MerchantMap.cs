using BankingSystem.DataTier.Entities;
using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace BankingSystem.DataTier.Mappings
{
    /// <summary>
    ///     Defines a mapping for the <see cref="Merchant" />
    /// </summary>
    public class MerchantMap : ClassMap<Merchant>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MerchantMap" /> class.
        /// </summary>
        public MerchantMap()
        {
            Table("Merchants");
            Id(x => x.Id).GeneratedBy.Guid();
            Map(x => x.Name).Not.Nullable();
            HasManyToMany<Account>(Reveal.Member<Merchant>("_accounts")).Table("MerchantAccounts").Inverse().Cascade.AllDeleteOrphan();
        }
    }
}