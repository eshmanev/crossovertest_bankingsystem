using BankingSystem.DAL.Entities;
using FluentNHibernate.Mapping;

namespace BankingSystem.DAL.Mappings
{
    /// <summary>
    ///     Defines a mapping for the <see cref="BankBalance" />.
    /// </summary>
    public class BankBalanceMap : ClassMap<BankBalance>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BankBalanceMap" /> class.
        /// </summary>
        public BankBalanceMap()
        {
            Table("BankBalance");
            Map(x => x.TotalBalance).Not.Nullable();
        }
    }
}