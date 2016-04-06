using BankingSystem.DataTier.Entities;
using FluentNHibernate.Mapping;

namespace BankingSystem.DataTier.Mappings
{
    /// <summary>
    ///     Defines a mapping for <see cref="Operation" />.
    /// </summary>
    public class OperationMap : ClassMap<Operation>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OperationMap" /> class.
        /// </summary>
        public OperationMap()
        {
            Table("Operations");
            Id(x => x.Id).GeneratedBy.HiLo<Operation>();
            Map(x => x.Amount).Not.Nullable();
            Map(x => x.Commission).Not.Nullable();
            Map(x => x.DateTimeCreated).Not.Nullable();
            Map(x => x.Description).Not.Nullable();
        }
    }
}