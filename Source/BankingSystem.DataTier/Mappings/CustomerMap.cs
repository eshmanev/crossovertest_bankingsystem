using BankingSystem.Domain.Impl;
using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace BankingSystem.DataTier.Mappings
{
    /// <summary>
    ///     Defines a mapping for the <see cref="CustomerBase" /> class.
    /// </summary>
    internal class CustomerMap : ClassMap<CustomerBase>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomerMap" /> class.
        /// </summary>
        public CustomerMap()
        {
            Table("Customers");
            Id(x => x.Id).GeneratedBy.HiLo<CustomerBase>();
            Map(x => x.UserName).Unique();
            Map(x => x.Email);
            Map(x => x.PasswordHash);

            HasMany<Account>(Reveal.Member<CustomerBase>("_accounts"))
                .KeyColumn("CustomerId").Not.KeyNullable()
                .ForeignKeyConstraintName("FK_Account_Customer")
                .Inverse().Cascade.AllDeleteOrphan();

            HasMany<LoginInfo>(Reveal.Member<CustomerBase>("_logins"))
                .KeyColumn("CustomerId").Not.KeyNullable()
                .ForeignKeyConstraintName("FK_LoginInfo_Customer")
                .Inverse().Cascade.AllDeleteOrphan();

            DiscriminateSubClassesOnColumn("CustomerType");
        }
    }
}