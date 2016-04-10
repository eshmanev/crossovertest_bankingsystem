using BankingSystem.Domain.Impl;
using FluentNHibernate.Mapping;

namespace BankingSystem.DataTier.Mappings
{
    /// <summary>
    ///     Defines mapping for the <see cref="DeliveredEmail" />
    /// </summary>
    internal class DeliveredEmailMap : ClassMap<DeliveredEmail>
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
            Map(x => x.Subject).Length(1000).Not.Nullable();
            Map(x => x.Body).Length(4000).Not.Nullable();
        }
    }
}