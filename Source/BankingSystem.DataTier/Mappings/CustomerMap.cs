﻿using BankingSystem.DataTier.Entities;
using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace BankingSystem.DataTier.Mappings
{
    /// <summary>
    ///  Defines a mapping for <see cref="Customer"/>
    /// </summary>
    /// <seealso cref="Customer" />
    public class CustomerMap : ClassMap<Customer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerMap"/> class.
        /// </summary>
        public CustomerMap()
        {
            Table("Customers");
            Id(x => x.Id).GeneratedBy.HiLo<Customer>();
            Map(x => x.UserName).Unique();
            Map(x => x.Email);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.PasswordHash);
            HasManyToMany<Account>(Reveal.Member<Customer>("_accounts")).Table("CustomerAccounts").Inverse().Cascade.AllDeleteOrphan();
            HasMany<LoginInfo>(Reveal.Member<Customer>("_logins")).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}