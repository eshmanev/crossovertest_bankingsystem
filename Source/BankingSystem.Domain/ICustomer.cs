using System.Collections.Generic;

namespace BankingSystem.Domain
{
    /// <summary>
    /// Defines a customer.
    /// </summary>
    public interface ICustomer
    {
        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        string FirstName { get; }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        string LastName { get; }

        /// <summary>
        /// Gets a collection of the customer's accounts.
        /// </summary>
        /// <value>
        /// The accounts.
        /// </value>
        IList<IAccount> Accounts { get; } 
    }
}