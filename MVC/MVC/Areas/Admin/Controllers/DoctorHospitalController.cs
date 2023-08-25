using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Admin.Extensions;
using MVC.Areas.Admin.Filters;
using MVC.Areas.Admin.HttpApiServices;
using MVC.Areas.Admin.Models;
using MVC.Areas.Admin.Models.Dtos.Department;
using MVC.Areas.Admin.Models.Dtos.Doctor;
using MVC.Areas.Admin.Models.Dtos.DoctorHospital;
using MVC.Areas.Admin.ViewModels;
using System.Text.Json;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAspect]
    public class DoctorHospitalController : Controller
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly IHttpApiService _httpApiService;
        public DoctorHospitalController(IWebHostEnvironment webHost, IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var doctorHospitalResponse = await _httpApiService.GetData<ResponseBody<List<DoctorHospitalItem>>>("/doctorHospital", token.Token);

            var hospitalResponse = await _httpApiService.GetData<ResponseBody<List<HospitalItem>>>("/hospital", token.Token);

            var doctorResponse = await _httpApiService.GetData<ResponseBody<List<DoctorItem>>>("/doctor", token.Token);

            var vm = new DoctorHospitalViewModel();
            vm.DoctorHospitals = doctorHospitalResponse.Data;
            vm.Hospitals = hospitalResponse.Data;
            vm.Doctors = doctorResponse.Data;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> GetDoctorHospital(int id)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var response =
              await _httpApiService.GetData<ResponseBody<DoctorHospitalItem>>($"/doctorHospital/{id}", token.Token);

            return Json(new
            {
                HospitalId = response.Data.HospitalId,
                DoctorId = response.Data.DoctorId,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Save(NewDoctorHospitalDto dto)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");


            var postObj = new
            {
                doctorId = dto.DoctorId,
                hospitalId = dto.HospitalId,
            };

            var response = await _httpApiService.PostData<ResponseBody<DoctorHospitalItem>>("/doctorHospital", JsonSerializer.Serialize(postObj), token.Token);

            if (response.StatusCode == 201)
            {
                return Json(
            new
            {
                IsSuccess = true,
                Message = "Doktor Hastaneye Başarıyla Kaydedildi",
                DoctorId = response.Data.DoctorId,
                HospitalId = response.Data.HospitalId,
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

            var response = await _httpApiService.DeleteData<ResponseBody<NoData>>($"/doctorHospital/{id}", token.Token);

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Kayıt Başarıyla Silindi" });

            return Json(new { IsSuccess = false, Message = "Kayıt Silinemedi", ErrorMessages = response.ErrorMessages });
        }

        public async Task<IActionResult> Update(UpdateDoctorHospitalDto dto)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var postObj = new
            {
                Id = dto.Id,
                DoctorId = dto.DoctorId,
                HospitalId = dto.HospitalId,
            };

            var response = await _httpApiService.PutData<ResponseBody<UpdateDoctorHospitalDto>>("/doctorHospital", JsonSerializer.Serialize(postObj), token.Token);

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
