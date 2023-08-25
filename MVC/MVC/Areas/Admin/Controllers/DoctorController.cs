using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Admin.Extensions;
using MVC.Areas.Admin.Filters;
using MVC.Areas.Admin.HttpApiServices;
using MVC.Areas.Admin.Models;
using MVC.Areas.Admin.Models.Dtos.Department;
using MVC.Areas.Admin.Models.Dtos.Doctor;
using MVC.Areas.Admin.Models.Dtos.Hospital;
using MVC.Areas.Admin.ViewModels;
using System.Text.Json;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAspect]
    public class DoctorController : Controller
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly IHttpApiService _httpApiService;
        public DoctorController(IWebHostEnvironment webHost, IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
            _webHost = webHost;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");


            var doctorResponse = await _httpApiService.GetData<ResponseBody<List<DoctorItem>>>("/doctor", token.Token);
            return View(doctorResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetDoctor(int id)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var response =
              await _httpApiService.GetData<ResponseBody<DoctorItem>>($"/doctor/{id}", token.Token);

            return Json(new
            {
                Name = response.Data.Name,
                Degree = response.Data.Degree,
                PicturePath = response.Data.PicturePath
            });
        }

        [HttpPost]
        public async Task<IActionResult> Save(NewDoctorDto dto, IFormFile doctorPhoto)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            if (doctorPhoto != null)
            {
                if (!doctorPhoto.ContentType.StartsWith("image/"))

                    return Json(new { IsSuccess = false, Message = "Doktor için sadece resim dosyası seçilmelidir." });

                var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(doctorPhoto.FileName)}";

                var picturePath = $@"/images/doctor/{randomFileName}";


                var upload = $@"{_webHost.ContentRootPath}/wwwroot{picturePath}";

                using (var fs = new FileStream(upload, FileMode.Create))
                {
                    doctorPhoto.CopyTo(fs);

                }

                var postObj = new
                {
                    name = dto.Name,
                    degree = dto.Degree,
                    PicturePath = picturePath,
                    PictureBase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(upload)),
                };

                var response = await _httpApiService.PostData<ResponseBody<DoctorItem>>("/doctor", JsonSerializer.Serialize(postObj), token.Token);

                if (response.StatusCode == 201)
                {
                    return Json(
                new
                {
                    IsSuccess = true,
                    Message = "Doktor Başarıyla Kaydedildi",
                    Name = response.Data.Name,
                    Degree = response.Data.Degree,
                    PicturePath = response.Data.PicturePath,
                    PictureBase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(upload)),
                });
                }
                else
                {
                    return Json(new { IsSuccess = false, Messages = response.ErrorMessages });
                }
            }
            else
            {

                return Json(new { IsSuccess = false, Message = "Doktor için resim seçilmelidir." });

            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var response = await _httpApiService.DeleteData<ResponseBody<NoData>>($"/department/{id}", token.Token);

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Kayıt Başarıyla Silindi" });

            return Json(new { IsSuccess = false, Message = "Kayıt Silinemedi", ErrorMessages = response.ErrorMessages });
        }

        public async Task<IActionResult> Update(UpdateDoctorDto dto, IFormFile doctorPhoto)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            if (doctorPhoto != null)
            {

                if (!doctorPhoto.ContentType.StartsWith("image/"))

                    return Json(new { IsSuccess = false, Message = "Doktor için sadece resim dosyası seçilmelidir." });

                var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(doctorPhoto.FileName)}";

                var picturePath = $@"/images/doctor/{randomFileName}";

                var upload = $@"{_webHost.ContentRootPath}/wwwroot{picturePath}";


                using (var fs = new FileStream(upload, FileMode.Create))
                {
                    doctorPhoto.CopyTo(fs);

                }
                var postObj = new
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Degree = dto.Degree,
                    PicturePath = picturePath,
                    PictureBase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(upload)),
                };

                var response = await _httpApiService.PutData<ResponseBody<UpdateDoctorDto>>("/doctor", JsonSerializer.Serialize(postObj), token.Token);

                if (response.StatusCode.ToString().StartsWith("2"))
                {
                    return Json(new { IsSuccess = true, Message = "Başarıyla Güncellendi" });

                }
                else
                {
                    return Json(new { IsSuccess = false, Messages = response.ErrorMessages });
                }
            }
            else
            {
                var postObj = new
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Degree = dto.Degree,
                };

                var response = await _httpApiService.PutData<ResponseBody<UpdateDoctorDto>>("/department", JsonSerializer.Serialize(postObj), token.Token);

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
}
