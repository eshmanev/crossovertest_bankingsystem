using System;

namespace BankingSystem.Data
{
    /// <summary>
    ///     Defines an operation.
    /// </summary>
    public interface IOperation
    {
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

        /// <summary>
        ///     Gets the amount.
        /// </summary>
        /// <value>
        ///     The amount.
        /// </value>
        decimal Amount { get; }

        /// <summary>
        ///     Gets the bank commission.
        /// </summary>
        /// <value>
        ///     The bank commission.
        /// </value>
        decimal Commission { get; }
    }
}