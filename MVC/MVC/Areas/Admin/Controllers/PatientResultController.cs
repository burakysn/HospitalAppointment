using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Admin.Extensions;
using MVC.Areas.Admin.HttpApiServices;
using MVC.Areas.Admin.Models.Dtos.Doctor;
using MVC.Areas.Admin.Models.Dtos.Hospital;
using MVC.Areas.Admin.Models;
using System.Text.Json;
using MVC.Areas.Admin.Models.Dtos.Department;
using MVC.Areas.Admin.ViewModels;
using MVC.Areas.Admin.Models.Dtos.PatientResult;
using MVC.Areas.Admin.Filters;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAspect]
    public class PatientResultController : Controller
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly IHttpApiService _httpApiService;
        public PatientResultController(IWebHostEnvironment webHost, IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
            _webHost = webHost;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var patientResultResponse = await _httpApiService.GetData<ResponseBody<List<PatientResultItem>>>("/patientResult", token.Token);
            var doctorResponse = await _httpApiService.GetData<ResponseBody<List<DoctorItem>>>("/doctor", token.Token);
            var patientResponse = await _httpApiService.GetData<ResponseBody<List<PatientItem>>>("/patient", token.Token);
            var hospitalAppointmentResponse = await _httpApiService.GetData<ResponseBody<List<HospitalAppointmentItem>>>("/hospitalAppointment", token.Token);
            var vm = new PatientResultViewModel();
            vm.PatientResult= patientResultResponse.Data;
            vm.Doctors = doctorResponse.Data;
            vm.Patients = patientResponse.Data;
            vm.HospitalAppointments = hospitalAppointmentResponse.Data;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> GetPatientResult(int id)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var response =
              await _httpApiService.GetData<ResponseBody<PatientResultItem>>($"/patientResult/{id}", token.Token);

            return Json(new
            {
                Description = response.Data.Description,
                DoctorId = response.Data.DoctorId,
                PatientId = response.Data.PatientId,
                HospitalAppointmentId = response.Data.HospitalAppointmentId,
                DoctorItem = response.Data.Doctors,
                Patients = response.Data.Patients,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Save(NewPatientResultDto dto)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var postObj = new
            {
                description = dto.Description,
                doctorId = dto.DoctorId,
                patientId = dto.PatientId,
                hospitalAppointmentId = dto.HospitalAppointmentId,
            };

            var response = await _httpApiService.PostData<ResponseBody<PatientResultItem>>("/patientResult", JsonSerializer.Serialize(postObj), token.Token);

            if (response.StatusCode == 201)
            {
                return Json(
            new
            {
                IsSuccess = true,
                Message = "Doktor Başarıyla Kaydedildi",
                Description = dto.Description,
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                HospitalAppointmentId = dto.HospitalAppointmentId,
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

            var response = await _httpApiService.DeleteData<ResponseBody<NoData>>($"/patientResult/{id}", token.Token);

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Kayıt Başarıyla Silindi" });

            return Json(new { IsSuccess = false, Message = "Kayıt Silinemedi", ErrorMessages = response.ErrorMessages });
        }

        public async Task<IActionResult> Update(UpdatePatientResultDto dto)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var postObj = new
            {
                Id = dto.Id,
                Description = dto.Description,
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                HospitalAppointmentId = dto.HospitalAppointmentId,
            };

            var response = await _httpApiService.PutData<ResponseBody<UpdatePatientResultDto>>("/patientResult", JsonSerializer.Serialize(postObj), token.Token);

            //ya da response.statuscode == 201
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
