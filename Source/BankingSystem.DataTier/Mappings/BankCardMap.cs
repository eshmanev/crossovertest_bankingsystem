using BankingSystem.DataTier.Entities;
using FluentNHibernate.Mapping;

namespace BankingSystem.DataTier.Mappings
{
    /// <summary>
    ///     Defines a mapping for <see cref="BankCard" />
    /// </summary>
    /// <seealso cref="FluentNHibernate.Mapping.ClassMap{BankingSystem.DAL.Entities.BankCard}" />
    public class BankCardMap : ClassMap<BankCard>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BankCardMap" /> class.
        /// </summary>
        public BankCardMap()
        {
            Table("BankCards");
            Id(x => x.Id).GeneratedBy.HiLo<BankCard>();
            Map(x => x.CardHolder);
            Map(x => x.CardNumber);
            Map(x => x.CsvCode);
            Map(x => x.ExpirationMonth);
            Map(x => x.ExpirationYear);
        }
    }
}