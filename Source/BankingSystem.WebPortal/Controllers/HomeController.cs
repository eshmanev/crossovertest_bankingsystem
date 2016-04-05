using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankingSystem.WebPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return !User.Identity.IsAuthenticated ? (ActionResult)RedirectToAction("Login", "Auth") : View("Dashboard");
        }
    }
}