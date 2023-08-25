using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Admin.Extensions;
using MVC.Areas.Admin.Filters;
using MVC.Areas.Admin.HttpApiServices;
using MVC.Areas.Admin.Models.Dtos.Department;
using MVC.Areas.Admin.Models.Dtos.Doctor;
using MVC.Areas.Admin.Models;
using MVC.Areas.Admin.ViewModels;
using System.Text.Json;
using MVC.Areas.Admin.Models.Dtos.DoctorHospital;
using MVC.Areas.Admin.Models.Dtos.HospitalAppointment;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAspect]
    public class HospitalAppointmentController : Controller
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly IHttpApiService _httpApiService;
        public HospitalAppointmentController(IWebHostEnvironment webHost, IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
            _webHost = webHost;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");


            var hospitalAppointmentResponse = await _httpApiService.GetData<ResponseBody<List<HospitalAppointmentItem>>>("/hospitalAppointment", token.Token);
            var doctorResponse = await _httpApiService.GetData<ResponseBody<List<DoctorItem>>>("/doctor", token.Token);
            var patientResponse = await _httpApiService.GetData<ResponseBody<List<PatientItem>>>("/patient", token.Token);
            var vm = new HospitalAppointmentViewModel();

            vm.HospitalAppointments = hospitalAppointmentResponse.Data;
            vm.Doctors = doctorResponse.Data;
            vm.Patients = patientResponse.Data;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> GetHospitalAppointment(int id)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var response =
              await _httpApiService.GetData<ResponseBody<HospitalAppointmentItem>>($"/hospitalAppointment/{id}", token.Token);

            return Json(new
            {
                DoctorId = response.Data.DoctorId,
                PatientId = response.Data.PatientId,
                Time = response.Data.Time.ToString("yyyy-mm-dd HH:mm:ss"),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Save(NewHospitalAppointmentDto dto)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var postObj = new
            {
                doctorId = dto.DoctorId,
                patientId = dto.PatientId,
                time = dto.Time,
            };

            var response = await _httpApiService.PostData<ResponseBody<HospitalAppointmentItem>>("/hospitalAppointment", JsonSerializer.Serialize(postObj), token.Token);

            if (response.StatusCode == 201)
            {
                return Json(
            new
            {
                IsSuccess = true,
                Message = "Randevu Başarıyla Kaydedildi",
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                Time = dto.Time,
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

            var response = await _httpApiService.DeleteData<ResponseBody<NoData>>($"/hospitalAppointment/{id}", token.Token);

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Kayıt Başarıyla Silindi" });

            return Json(new { IsSuccess = false, Message = "Kayıt Silinemedi", ErrorMessages = response.ErrorMessages });
        }

        public async Task<IActionResult> Update(UpdateHospitalAppointmentDto dto)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var postObj = new
            {
                Id = dto.Id,
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                Time = dto.Time,
            };

            var response = await _httpApiService.PutData<ResponseBody<UpdateHospitalAppointmentDto>>("/hospitalAppointment", JsonSerializer.Serialize(postObj), token.Token);

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
