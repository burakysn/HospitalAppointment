using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Admin.HttpApiServices;
using MVC.Areas.Admin.Models;
using MVC.Areas.Admin.ViewModels;
using MVC.Models;
using NuGet.Common;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        public HomeController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        public async Task<IActionResult> Index()
        {
            var doctorResponse = await _httpApiService.GetData<ResponseBody<List<DoctorItem>>>("/doctor");
            var departmentResponse = await _httpApiService.GetData<ResponseBody<List<DepartmentItem>>>("/department");
            var patientResponse = await _httpApiService.GetData<ResponseBody<List<PatientItem>>>("/patient");

            var vm = new GlobalViewModel();

            vm.Doctors = doctorResponse.Data;
            vm.Departments = departmentResponse.Data;
            vm.Patients = patientResponse.Data;

            vm.DoctorCount = doctorResponse.Data.Count;
            vm.DepartmentCount = departmentResponse.Data.Count;
            vm.PatientCount = patientResponse.Data.Count;

            return View(vm);
        }
    }
}