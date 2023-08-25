using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using MVC.Areas.Admin.Extensions;
using MVC.Areas.Admin.Models;

namespace MVC.Areas.Patient.ViewComponents
{
    public class SideBarPatientViewComponent : ViewComponent
    {
        public ViewViewComponentResult Invoke()
        {
            var patient = HttpContext.Session.GetObject<PatientItem>("ActivePatient");

            return View(patient);
        }
    }
}
