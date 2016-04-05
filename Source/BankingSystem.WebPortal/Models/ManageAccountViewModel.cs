using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace BankingSystem.WebPortal.Models
{
    public class ManageAccountViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
    }
}