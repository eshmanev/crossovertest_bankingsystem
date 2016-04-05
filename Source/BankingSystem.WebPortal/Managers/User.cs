using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using BankingSystem.Data;
using BankingSystem.Domain;
using Microsoft.AspNet.Identity;

namespace BankingSystem.WebPortal.Managers
{
    /// <summary>
    /// Represents a user for the current web application.
    /// </summary>
    public class User : IUser<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public User(ICustomer customer)
        {
            Id = customer.Id;
            UserName = customer.UserName;
        }

        /// <summary>
        /// Unique key for the user
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Unique username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets the claims identity.
        /// </summary>
        /// <returns>An instance of the <see cref="ClaimsIdentity"/> class.</returns>
        public Task<ClaimsIdentity> GetCliemsIdentityAsync()
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Id.ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer32));
            return Task.FromResult(identity);
        }
    }
}