using Microsoft.AspNetCore.Mvc;

namespace CateringOrderWebApplication.Controllers
{
    public class PlannerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}