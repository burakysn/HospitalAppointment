using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Admin.Extensions;
using MVC.Areas.Admin.Filters;
using MVC.Areas.Admin.HttpApiServices;
using MVC.Areas.Admin.Models;
using MVC.Areas.Admin.Models.Dtos.Doctor;
using MVC.Areas.Admin.Models.Dtos.Hospital;
using MVC.Areas.Admin.Models.Dtos.Patient;
using System.Text.Json;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAspect]
    public class PatientController : Controller
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly IHttpApiService _httpApiService;
        public PatientController(IWebHostEnvironment webHost, IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");


            var response = await _httpApiService.GetData<ResponseBody<List<PatientItem>>>("/patient", token.Token);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetPatient(int id)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");
            var response =
        await _httpApiService.GetData<ResponseBody<PatientItem>>($"/patient/{id}", token.Token);

            return Json(new { Name = response.Data.Name, SurName = response.Data.SurName, Email = response.Data.Email, Password = response.Data.Password, TcNo = response.Data.TcNo, PhoneNumber = response.Data.PhoneNumber });
        }

        [HttpPost]
        public async Task<IActionResult> Save(NewPatientDto dto)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var name = dto.Name;
            var surName = dto.SurName;
            var email = dto.Email;
            var password = dto.Password;
            var tcNo = dto.TcNo;
            var phoneNumber = dto.PhoneNumber;

            var postObj = new
            {
                name = dto.Name,
                surName = dto.SurName,
                email = dto.Email,
                password = dto.Password,
                tcNo = dto.TcNo,
                phoneNumber = dto.PhoneNumber,
            };

            var response = await _httpApiService.PostData<ResponseBody<PatientItem>>("/patient", JsonSerializer.Serialize(postObj), token.Token);

            if (response.StatusCode == 201)
            {
                return Json(
            new
            {
                IsSuccess = true,
                Message = "Hasta Başarıyla Kaydedildi",
                Name = dto.Name,
                SurName = dto.SurName,
                Email = dto.Email,
                Password = dto.Password,
                TcNo = dto.TcNo,
                PhoneNumber = dto.PhoneNumber,
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
            var response = await _httpApiService.DeleteData<ResponseBody<NoData>>($"/patient/{id}");

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Kayıt Başarıyla Silindi" });

            return Json(new { IsSuccess = false, Message = "Kayıt Silinemedi", ErrorMessages = response.ErrorMessages });
        }

        public async Task<IActionResult> Update(UpdatePatientDto dto)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var postObj = new
            {
                name = dto.Name,
                surName = dto.SurName,
                email = dto.Email,
                password = dto.Password,
                tcNo = dto.TcNo,
                phoneNumber = dto.PhoneNumber,
            };

            var response = await _httpApiService.PutData<ResponseBody<UpdatePatientDto>>("/patient", JsonSerializer.Serialize(postObj), token.Token);

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
