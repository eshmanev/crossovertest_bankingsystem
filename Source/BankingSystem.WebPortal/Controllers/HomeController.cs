using System.Web.Mvc;

namespace BankingSystem.WebPortal.Controllers
{
    /// <summary>
    ///     Represents a controller which redirects users to a specific home page depending on authentication state.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [RequireHttps]
    public class HomeController : Controller
    {
        /// <summary>
        ///     Redirects to a home page.
        /// </summary>
        public ActionResult Index()
        {
            return !User.Identity.IsAuthenticated
                ? RedirectToAction("Login", "Auth")
                : RedirectToAction("Index", "Account");
        }
    }
}