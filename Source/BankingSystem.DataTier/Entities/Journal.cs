using System;
using BankingSystem.Common.Data;

namespace BankingSystem.DataTier.Entities
{
    /// <summary>
    ///     Represents an journal.
    /// </summary>
    /// <seealso cref="IJournal" />
    public class Journal : IJournal
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Journal" /> class.
        /// </summary>
        protected Journal()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Journal" /> class.
        /// </summary>
        /// <param name="dateTimeCreated">The date time created.</param>
        /// <param name="customer">The customer.</param>
        /// <param name="description">The description.</param>
        public Journal(DateTime dateTimeCreated, ICustomer customer, string description)
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            DateTimeCreated = dateTimeCreated;
            Description = description;
            Customer = customer;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public virtual int Id { get; protected set; }

        /// <summary>
        ///     Gets the customer.
        /// </summary>
        /// <value>
        ///     The customer.
        /// </value>
        public virtual ICustomer Customer { get; protected set; }

        /// <summary>
        ///     Gets the date and time created.
        /// </summary>
        /// <value>
        ///     The date and time created.
        /// </value>
        public virtual DateTime DateTimeCreated { get; protected set; }

        /// <summary>
        ///     Gets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public virtual string Description { get; protected set; }
    }
}