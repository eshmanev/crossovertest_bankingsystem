using System.Web.Mvc;

namespace BankingSystem.WebPortal.Controllers
{
    /// <summary>
    ///     Represents a controller of error pages.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class ErrorController : Controller
    {
        /// <summary>
        ///     Common error page.
        /// </summary>
        public ViewResult Index()
        {
            return View("Error");
        }

        /// <summary>
        ///     Not found error page.
        /// </summary>
        /// <returns></returns>
        public ViewResult NotFound()
        {
            Response.StatusCode = 404; //you may want to set this to 200
            return View("NotFound");
        }
    }
}