using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Admin.Filters;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAspect]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
