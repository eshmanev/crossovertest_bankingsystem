using System;

namespace BankingSystem.Domain.Impl
{
    /// <summary>
    ///     Represents a scheduled email.
    /// </summary>
    /// <seealso cref="IScheduledEmail" />
    public class ScheduledEmail : IScheduledEmail
    {
        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public virtual int Id { get; protected set; }

        /// <summary>
        ///     Gets or sets the recipient address.
        /// </summary>
        /// <value>
        ///     The recipient address.
        /// </value>
        public virtual string RecipientAddress { get; set; }

        /// <summary>
        ///     Gets or sets the scheduled date time.
        /// </summary>
        /// <value>
        ///     The scheduled date time.
        /// </value>
        public virtual DateTime ScheduledDateTime { get; set; }

        /// <summary>
        ///     Gets the subject.
        /// </summary>
        /// <value>
        ///     The subject.
        /// </value>
        public virtual string Subject { get; set; }

        /// <summary>
        ///     Gets the body.
        /// </summary>
        /// <value>
        ///     The body.
        /// </value>
        public virtual string Body { get; set; }

        /// <summary>
        ///     Gets or sets the last failure reason.
        /// </summary>
        /// <value>
        ///     The failure reason.
        /// </value>
        public virtual string FailureReason { get; set; }
    }
}