using BankingSystem.DataTier.Entities;
using FluentNHibernate.Mapping;

namespace BankingSystem.DataTier.Mappings
{
    /// <summary>
    ///     Defines a mapping for <see cref="Account" />
    /// </summary>
    /// <seealso cref="FluentNHibernate.Mapping.ClassMap{BankingSystem.DAL.Entities.Account}" />
    public class AccountMap : ClassMap<Account>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountMap" /> class.
        /// </summary>
        public AccountMap()
        {
            Table("Accounts");
            Id(x => x.Id).GeneratedBy.HiLo<Account>();
            Map(x => x.AccountNumber).Unique();
            Map(x => x.Currency).Not.Nullable();
            Map(x => x.Balance).Not.Nullable();
            HasOne(x => x.BankCard).Class<BankCard>();
            References(x => x.Customer).Class<Customer>();
        }
    }
}