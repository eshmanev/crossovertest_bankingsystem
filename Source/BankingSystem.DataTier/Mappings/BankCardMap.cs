﻿using BankingSystem.DataTier.Entities;
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
            Map(x => x.CardHolder).Not.Nullable();
            Map(x => x.CardNumber).Not.Nullable();
            Map(x => x.CsvCode).Not.Nullable();
            Map(x => x.ExpirationMonth).Not.Nullable();
            Map(x => x.ExpirationYear).Not.Nullable();
            Map(x => x.PinCode).Not.Nullable();
            HasOne(x => x.Account).Class<Account>();
        }
    }
}