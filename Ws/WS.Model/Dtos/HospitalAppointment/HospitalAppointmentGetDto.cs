using Infrastructure.Model;
using WS.Model.Dtos.Doctor;
using WS.Model.Dtos.User;

namespace WS.Model.Dtos.HospitalAppointment
{
    public class HospitalAppointmentGetDto : IDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime Time { get; set; }
        public DoctorGetDto Doctors { get; set; }
        public PatientGetDto Patients { get; set; }
    }
}
