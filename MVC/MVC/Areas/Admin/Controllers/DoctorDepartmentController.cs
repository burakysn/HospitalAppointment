using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Admin.Extensions;
using MVC.Areas.Admin.Filters;
using MVC.Areas.Admin.HttpApiServices;
using MVC.Areas.Admin.Models;
using MVC.Areas.Admin.Models.Dtos.DoctorDepartment;
using MVC.Areas.Admin.ViewModels;
using System.Text.Json;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAspect]
    public class DoctorDepartmentController : Controller
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly IHttpApiService _httpApiService;
        public DoctorDepartmentController(IWebHostEnvironment webHost, IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
            _webHost = webHost;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");


            var doctorDepartmentResponse = await _httpApiService.GetData<ResponseBody<List<DoctorDepartmentItem>>>("/doctorDepartment", token.Token);
            var departmentResponse = await _httpApiService.GetData<ResponseBody<List<DepartmentItem>>>("/department", token.Token);
            var doctorResponse = await _httpApiService.GetData<ResponseBody<List<DoctorItem>>>("/doctor", token.Token);
            var vm = new DoctorDepartmentViewModel();
            //vm.Users = users;
            vm.DoctorDepartments = doctorDepartmentResponse.Data;
            vm.Departments = departmentResponse.Data;
            vm.Doctors = doctorResponse.Data;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> GetDoctorDepartment(int id)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var response =
              await _httpApiService.GetData<ResponseBody<DoctorDepartmentItem>>($"/doctorDepartment/{id}", token.Token);

            return Json(new
            {
                DoctorId = response.Data.DoctorId,
                DepartmentId = response.Data.DepartmentId,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Save(NewDoctorDepartmentDto dto)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");


            var postObj = new
            {
                doctorId = dto.DoctorId,
                departmentId = dto.DepartmentId,
            };

            var response = await _httpApiService.PostData<ResponseBody<DoctorDepartmentItem>>("/doctorDepartment", JsonSerializer.Serialize(postObj), token.Token);

            if (response.StatusCode == 201)
            {
                return Json(
            new
            {
                IsSuccess = true,
                Message = "Doktor Departmana Başarıyla Kaydedildi",
                DoctorId = dto.DoctorId,
                DepartmentId = dto.DepartmentId,
            });
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var response = await _httpApiService.DeleteData<ResponseBody<NoData>>($"/doctorDepartment/{id}", token.Token);

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Kayıt Başarıyla Silindi" });

            return Json(new { IsSuccess = false, Message = "Kayıt Silinemedi", ErrorMessages = response.ErrorMessages });
        }

        public async Task<IActionResult> Update(UpdateDoctorDepartmentDto dto)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var postObj = new
            {
                Id = dto.Id,
                DepartmentId = dto.DepartmentId,
                DoctorId = dto.DoctorId,
            };

            var response = await _httpApiService.PutData<ResponseBody<UpdateDoctorDepartmentDto>>("/doctorDepartment", JsonSerializer.Serialize(postObj), token.Token);

            if (response.StatusCode.ToString().StartsWith("2"))
            {
                return Json(new { IsSuccess = true, Message = "Başarıyla Güncellendi" });

            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });
            }





        }
    }
}
