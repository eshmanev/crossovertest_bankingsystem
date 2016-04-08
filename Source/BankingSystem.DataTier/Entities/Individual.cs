using BankingSystem.Common.Data;

namespace BankingSystem.DataTier.Entities
{
    /// <summary>
    ///     Represents an individual.
    /// </summary>
    public class Individual : CustomerBase, IIndividual
    {
        /// <summary>
        ///     Gets the first name.
        /// </summary>
        /// <value>
        ///     The first name.
        /// </value>
        public virtual string FirstName { get; protected set; }

        /// <summary>
        ///     Gets the last name.
        /// </summary>
        /// <value>
        ///     The last name.
        /// </value>
        public virtual string LastName { get; protected set; }

        /// <summary>
        ///     Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Accept(ICustomerVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}