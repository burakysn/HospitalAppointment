using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Patient.Filter;

namespace MVC.Areas.Patient.Controllers
{
    [Area("Patient")]
    [SessionAspect]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
