using System;

namespace BankingSystem.Domain.Impl
{
    /// <summary>
    ///     Represents an individual.
    /// </summary>
    internal class Individual : CustomerBase, IIndividual
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Individual" /> class.
        /// </summary>
        protected Individual()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Individual" /> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="email">The email.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public Individual(string userName, string email, string passwordHash, string firstName, string lastName)
            : base(userName, email, passwordHash)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException(nameof(lastName));

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            FirstName = firstName;
            LastName = lastName;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        /// <summary>
        ///     Gets the first name.
        /// </summary>
        /// <value>
        ///     The first name.
        /// </value>
        public virtual string FirstName { get; protected set; }

        /// <summary>
        ///     Gets the last name.
        /// </summary>
        /// <value>
        ///     The last name.
        /// </value>
        public virtual string LastName { get; protected set; }

        /// <summary>
        ///     Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Accept(ICustomerVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}