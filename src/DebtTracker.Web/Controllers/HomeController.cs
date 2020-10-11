using Microsoft.AspNetCore.Mvc;

namespace DebtTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
