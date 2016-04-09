using BankingSystem.Domain.Impl;
using FluentNHibernate.Mapping;

namespace BankingSystem.DataTier.Mappings
{
    /// <summary>
    ///     Defines a mapping for the <see cref="BankBalance" />.
    /// </summary>
    internal class BankBalanceMap : ClassMap<BankBalance>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BankBalanceMap" /> class.
        /// </summary>
        public BankBalanceMap()
        {
            Table("BankBalance");
            Id(x => x.Id);
            Map(x => x.TotalBalance).Not.Nullable();
        }
    }
}