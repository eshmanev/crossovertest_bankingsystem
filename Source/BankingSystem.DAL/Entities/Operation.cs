using System;
using BankingSystem.Data;

namespace BankingSystem.DAL.Entities
{
    /// <summary>
    ///     Represents an operation.
    /// </summary>
    /// <seealso cref="BankingSystem.Data.IOperation" />
    public class Operation : IOperation
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Operation" /> class.
        /// </summary>
        protected Operation()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Operation" /> class.
        /// </summary>
        /// <param name="dateTimeCreated">The date time created.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="commission">The commission.</param>
        /// <param name="description">The description.</param>
        public Operation(DateTime dateTimeCreated, decimal amount, decimal commission, string description)
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            DateTimeCreated = dateTimeCreated;
            Description = description;
            Amount = amount;
            Commission = commission;
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

        /// <summary>
        ///     Gets the amount.
        /// </summary>
        /// <value>
        ///     The amount.
        /// </value>
        public virtual decimal Amount { get; protected set; }

        /// <summary>
        ///     Gets the bank commission.
        /// </summary>
        /// <value>
        ///     The bank commission.
        /// </value>
        public virtual decimal Commission { get; protected set; }
    }
}