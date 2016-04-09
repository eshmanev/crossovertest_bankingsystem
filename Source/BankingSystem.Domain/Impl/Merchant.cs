using System;

namespace BankingSystem.Domain.Impl
{
    /// <summary>
    ///     Represents a merchant.
    /// </summary>
    /// <seealso cref="IMerchant" />
    internal class Merchant : CustomerBase, IMerchant
    {
        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public virtual Guid MerchantId { get; protected set; }

        /// <summary>
        ///     Gets the merchant's name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public virtual string MerchantName { get; protected set; }

        /// <summary>
        ///     Gets the contact person.
        /// </summary>
        /// <value>
        ///     The contact person.
        /// </value>
        public virtual string ContactPerson { get; protected set; }

        /// <summary>
        ///     Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(ICustomerVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}