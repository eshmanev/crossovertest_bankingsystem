using BankingSystem.DataTier.Entities;
using FluentNHibernate.Mapping;

namespace BankingSystem.DataTier.Mappings
{
    /// <summary>
    ///     Defines mapping for the <see cref="ScheduledEmail" />
    /// </summary>
    public class ScheduledEmailMap : ClassMap<ScheduledEmail>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ScheduledEmailMap" /> class.
        /// </summary>
        public ScheduledEmailMap()
        {
            Table("ScheduledEmails");
            Id(x => x.Id);
            Map(x => x.RecipientAddress).Not.Nullable();
            Map(x => x.ScheduledDateTime).Not.Nullable();
            Map(x => x.Subject).Not.Nullable();
            Map(x => x.Body).Not.Nullable();
            Map(x => x.FailureReason).Nullable();
        }
    }
}