using Microsoft.AspNetCore.Mvc;

namespace sait.Controllers
{
    public class Home1Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
