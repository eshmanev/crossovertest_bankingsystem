using System;

namespace BankingSystem.Common.Data
{
    /// <summary>
    ///     Defines an operation.
    /// </summary>
    public interface IJournal
    {
        /// <summary>
        ///     Gets the customer.
        /// </summary>
        /// <value>
        ///     The customer.
        /// </value>
        ICustomer Customer { get; }

        /// <summary>
        ///     Gets the date and time created.
        /// </summary>
        /// <value>
        ///     The date and time created.
        /// </value>
        DateTime DateTimeCreated { get; }

        /// <summary>
        ///     Gets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        string Description { get; }
    }
}