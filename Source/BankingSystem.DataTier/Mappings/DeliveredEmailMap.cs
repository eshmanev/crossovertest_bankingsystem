﻿using BankingSystem.DataTier.Entities;
using FluentNHibernate.Mapping;

namespace BankingSystem.DataTier.Mappings
{
    /// <summary>
    ///     Defines mapping for the <see cref="DeliveredEmail" />
    /// </summary>
    public class DeliveredEmailMap : ClassMap<DeliveredEmail>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DeliveredEmailMap" /> class.
        /// </summary>
        public DeliveredEmailMap()
        {
            Table("DeliveredEmails");
            Id(x => x.Id);
            Map(x => x.RecipientAddress).Not.Nullable();
            Map(x => x.DeliveredDateTime).Not.Nullable();
            Map(x => x.Subject).Not.Nullable();
            Map(x => x.Body).Not.Nullable();
        }
    }
}