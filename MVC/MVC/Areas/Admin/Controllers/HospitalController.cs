using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Admin.Extensions;
using MVC.Areas.Admin.Filters;
using MVC.Areas.Admin.HttpApiServices;
using MVC.Areas.Admin.Models;
using MVC.Areas.Admin.Models.Dtos.Hospital;
using System.Text.Json;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAspect]
    public class HospitalController : Controller
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly IHttpApiService _httpApiService;
        public HospitalController(IWebHostEnvironment webHost, IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");
            var response = await _httpApiService.GetData<ResponseBody<List<HospitalItem>>>("/hospital", token.Token);

            return View(response.Data);
        }
        public async Task<IActionResult> GetHospital(int id)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var response =
        await _httpApiService.GetData<ResponseBody<HospitalItem>>($"/hospital/{id}", token.Token);

            return Json(new { Name = response.Data.Name, Address = response.Data.Address, PhoneNumber = response.Data.PhoneNumber, PicturePath = response.Data.PicturePath });
        }

        [HttpPost]
        public async Task<IActionResult> Save(NewHospitalDto dto, IFormFile hospitalPhoto)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");
            if (hospitalPhoto != null)
            {
                if (!hospitalPhoto.ContentType.StartsWith("image/"))

                    return Json(new { IsSuccess = false, Message = "Hastane için sadece resim dosyası seçilmelidir." });

                var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(hospitalPhoto.FileName)}";

                var picturePath = $@"/images/hospital/{randomFileName}";

                //dosya yolu
                var upload = $@"{_webHost.ContentRootPath}/wwwroot{picturePath}";

                using (var fs = new FileStream(upload, FileMode.Create))
                {
                    hospitalPhoto.CopyTo(fs);

                }
                var name = dto.Name;
                var address = dto.Address;
                var phoneNumber = dto.PhoneNumber;

                var postObj = new
                {
                    Name = name,
                    Address = address,
                    PhoneNumber = phoneNumber,
                    PicturePath = picturePath,
                    PictureBase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(upload)),
                };

                var response = await _httpApiService.PostData<ResponseBody<HospitalItem>>("/hospital", JsonSerializer.Serialize(postObj), token.Token);

                if (response.StatusCode == 201)
                {
                    return Json(
                new
                {
                    IsSuccess = true,
                    Message = "Kategori Başarıyla Kaydedildi",
                    Name = response.Data.Name,
                    Address = response.Data.Address,
                    PhoneNumber = response.Data.PhoneNumber,
                });
                }
                else
                {
                    return Json(new { IsSuccess = false, Messages = response.ErrorMessages });
                }
            }
            else
            {

                return Json(new { IsSuccess = false, Message = "Hastane için resim seçilmelidir." });

            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            var response = await _httpApiService.DeleteData<ResponseBody<NoData>>($"/hospital/{id}", token.Token);

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Kayıt Başarıyla Silindi" });

            return Json(new { IsSuccess = false, Message = "Kayıt Silinemedi", ErrorMessages = response.ErrorMessages });
        }

        public async Task<IActionResult> Update(UpdateHospitalDto dto, IFormFile hospitalPhoto)
        {
            var token = HttpContext.Session.GetObject<AccessTokenItem>("AccessToken");

            if (hospitalPhoto != null)
            {

                if (!hospitalPhoto.ContentType.StartsWith("image/"))

                    return Json(new { IsSuccess = false, Message = "Hastane için sadece resim dosyası seçilmelidir." });

                var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(hospitalPhoto.FileName)}";

                var picturePath = $@"/images/hospital/{randomFileName}";

                //dosya yolu
                var upload = $@"{_webHost.ContentRootPath}/wwwroot{picturePath}";


                using (var fs = new FileStream(upload, FileMode.Create))
                {
                    hospitalPhoto.CopyTo(fs);

                }

                var postObj = new
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Address = dto.Address,
                    PhoneNumber = dto.PhoneNumber,
                    PicturePath = picturePath,
                    PictureBase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(upload)),
                };

                var response = await _httpApiService.PutData<ResponseBody<UpdateHospitalDto>>("/hospital", JsonSerializer.Serialize(postObj), token.Token);

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
                    Address = dto.Address,
                    PhoneNumber = dto.PhoneNumber,
                };

                var response = await _httpApiService.PutData<ResponseBody<UpdateHospitalDto>>("/hospital", JsonSerializer.Serialize(postObj), token.Token);

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
