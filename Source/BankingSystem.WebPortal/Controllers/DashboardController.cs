using System.Web.Mvc;

namespace BankingSystem.WebPortal.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}