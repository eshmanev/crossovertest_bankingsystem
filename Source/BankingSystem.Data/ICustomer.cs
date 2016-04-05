using System.Collections.Generic;

namespace BankingSystem.Data
{
    /// <summary>
    /// Defines a customer.
    /// </summary>
    public interface ICustomer
    {
        /// <summary>
        /// Gets the entity identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        int Id { get; }

        /// <summary>
        /// Gets the unique username.
        /// </summary>
        /// <value>
        /// The unique username.
        /// </value>
        string UserName { get; }

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
        IEnumerable<IAccount> Accounts { get; }

        /// <summary>
        /// Gets a collection of the customer's logins.
        /// </summary>
        /// <value>
        /// The logins.
        /// </value>
        IEnumerable<ILoginInfo> Logins { get; } 
    }
}