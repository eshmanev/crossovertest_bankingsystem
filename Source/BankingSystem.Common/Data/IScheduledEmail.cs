using System;

namespace BankingSystem.Common.Data
{
    /// <summary>
    ///     Defines a scheduled email.
    /// </summary>
    public interface IScheduledEmail
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
        DateTime ScheduledDateTime { get; }

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

        /// <summary>
        ///     Gets or sets the last failure reason.
        /// </summary>
        /// <value>
        ///     The failure reason.
        /// </value>
        string FailureReason { get; set; }
    }
}