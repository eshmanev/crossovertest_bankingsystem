﻿using FluentNHibernate.Mapping;

namespace BankingSystem.DAL.Mappings
{
    /// <summary>
    /// Contains extension methods for identity generation strategy builder.
    /// </summary>
    internal static class MappingExtensions
    {
        /// <summary>
        /// Assignes HiLo key generation strategy.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="generatedBy">The generated by.</param>
        /// <returns></returns>
        public static IdentityPart HiLo<TEntity>(this IdentityGenerationStrategyBuilder<IdentityPart> generatedBy)
            where TEntity : class
        {
            return generatedBy.HiLo("HiLo", "NextHi", "100", $"Entity = '{typeof(TEntity).Name}'");
        }
    }
}