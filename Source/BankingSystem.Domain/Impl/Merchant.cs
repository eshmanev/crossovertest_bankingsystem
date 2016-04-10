using System;

namespace BankingSystem.Domain.Impl
{
    /// <summary>
    ///     Represents a merchant.
    /// </summary>
    /// <seealso cref="IMerchant" />
    public class Merchant : CustomerBase, IMerchant
    {
        protected Merchant()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Merchant" /> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="email">The email.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="merchantName">Name of the merchant.</param>
        /// <param name="contactPerson">The contact person.</param>
        public Merchant(string userName, string email, string passwordHash, Guid merchantId, string merchantName, string contactPerson)
            : base(userName, email, passwordHash)
        {
            if (Guid.Empty == merchantId)
                throw new ArgumentException(nameof(merchantId));

            if (string.IsNullOrWhiteSpace(merchantName))
                throw new ArgumentNullException(nameof(merchantName));

            if (string.IsNullOrWhiteSpace(contactPerson))
                throw new ArgumentNullException(nameof(contactPerson));

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            MerchantId = merchantId;
            MerchantName = merchantName;
            ContactPerson = contactPerson;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

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