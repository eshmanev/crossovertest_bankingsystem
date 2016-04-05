using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace BankingSystem.WebPortal.Models
{
    /// <summary>
    /// Represents a view model for managing customer social logins.
    /// </summary>
    public class ManageLoginsViewModel
    {
        /// <summary>
        /// Gets or sets the current logins.
        /// </summary>
        /// <value>
        /// The current logins.
        /// </value>
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        /// <summary>
        /// Gets or sets the other logins.
        /// </summary>
        /// <value>
        /// The other logins.
        /// </value>
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }
}