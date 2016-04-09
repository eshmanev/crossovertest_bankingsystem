using BankingSystem.Domain.Impl;
using FluentNHibernate.Mapping;

namespace BankingSystem.DataTier.Mappings
{
    /// <summary>
    ///     Defines a mapping for the <see cref="Individual" /> class
    /// </summary>
    internal class IndividualMap : SubclassMap<Individual>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomerMap" /> class.
        /// </summary>
        public IndividualMap()
        {
            DiscriminatorValue("individual");
            Map(x => x.FirstName);
            Map(x => x.LastName);
        }
    }
}