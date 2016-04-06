using System;

namespace BankingSystem.Common.Data
{
    /// <summary>
    ///     Defines a delivered email.
    /// </summary>
    public interface IDeliveredEmail
    {
        /// <summary>
        ///     Gets or sets the recipient address.
        /// </summary>
        /// <value>
        ///     The recipient address.
        /// </value>
        string RecipientAddress { get; }

        /// <summary>
        ///     Gets or sets the scheduled date time.
        /// </summary>
        /// <value>
        ///     The scheduled date time.
        /// </value>
        DateTime DeliveredDateTime { get; }

        /// <summary>
        ///     Gets the subject.
        /// </summary>
        /// <value>
        ///     The subject.
        /// </value>
        string Subject { get; }

        /// <summary>
        ///     Gets the body.
        /// </summary>
        /// <value>
        ///     The body.
        /// </value>
        string Body { get; }
    }
}