using Infrastructure.Model;
using WS.Model.Dtos.Doctor;
using WS.Model.Dtos.Hospital;
using WS.Model.Dtos.User;

namespace WS.Model.Dtos.DoctorHospital
{
    public class DoctorHospitalGetDto : IDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int HospitalId { get; set; }
        public DoctorGetDto Doctors { get; set; }
        public HospitalGetDto Hospitals { get; set; }
    }
}
