using Microsoft.AspNetCore.Mvc;

namespace DebtTracker.Web.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
