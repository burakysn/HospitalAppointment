using Infrastructure.Model;

namespace WS.Model.Dtos.DoctorHospital
{
    public class DoctorHospitalPutDto : IDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int HospitalId { get; set; }
    }
}
