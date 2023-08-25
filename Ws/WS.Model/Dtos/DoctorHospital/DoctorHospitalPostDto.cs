using Infrastructure.Model;

namespace WS.Model.Dtos.DoctorHospital
{
    public class DoctorHospitalPostDto : IDto
    {
        public int DoctorId { get; set; }
        public int HospitalId { get; set; }
    }
}
