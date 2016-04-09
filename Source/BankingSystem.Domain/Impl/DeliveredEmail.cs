﻿using System;

namespace BankingSystem.Domain.Impl
{
    /// <summary>
    ///     Represents a delivered email.
    /// </summary>
    /// <seealso cref="IDeliveredEmail" />
    internal class DeliveredEmail : IDeliveredEmail
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
        public virtual DateTime DeliveredDateTime { get; set; }

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
    }
}