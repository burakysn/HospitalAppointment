using Infrastructure.Model;

namespace WS.Model.Dtos.Hospital
{
    public class HospitalPostDto : IDto
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public Int64? PhoneNumber { get; set; }
        public string? PicturePath { get; set; }
        public string? PictureBase64 { get; set; }
    }
}
