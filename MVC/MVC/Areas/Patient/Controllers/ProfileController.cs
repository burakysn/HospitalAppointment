using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Admin.Extensions;
using MVC.Areas.Admin.HttpApiServices;
using MVC.Areas.Admin.Models;
using MVC.Areas.Admin.ViewModels;
using MVC.Areas.Patient.Filter;

namespace MVC.Areas.Patient.Controllers
{
    [Area("Patient")]
    [SessionAspect]
    public class ProfileController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        public ProfileController( IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }
        public async Task<IActionResult> Index(int id)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var response =
              await _httpApiService.GetData<ResponseBody<PatientItem>>($"/patient/{id}", token.Token);

            return View(response.Data);
        }
    }
}
