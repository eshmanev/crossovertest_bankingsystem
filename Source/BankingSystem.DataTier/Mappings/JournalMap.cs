using BankingSystem.Domain.Impl;
using FluentNHibernate.Mapping;

namespace BankingSystem.DataTier.Mappings
{
    /// <summary>
    ///     Defines a mapping for <see cref="Journal" />.
    /// </summary>
    internal class JournalMap : ClassMap<Journal>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JournalMap" /> class.
        /// </summary>
        public JournalMap()
        {
            Table("Journals");
            Id(x => x.Id).GeneratedBy.HiLo<Journal>();
            Map(x => x.DateTimeCreated).Not.Nullable();
            Map(x => x.Description).Length(4000).Not.Nullable();
            References(x => x.Customer).Class<CustomerBase>()
                .Column("CustomerId").Not.Nullable()
                .ForeignKey("FK_Journal_Customer");
        }
    }
}