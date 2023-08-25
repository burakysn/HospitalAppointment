using Infrastructure.Model;
using WS.Model.Dtos.Doctor;
using WS.Model.Dtos.HospitalAppointment;
using WS.Model.Dtos.User;

namespace WS.Model.Dtos.PatientResult
{
    public class PatientResultGetDto : IDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int HospitalAppointmentId { get; set; }
        public DoctorGetDto Doctors { get; set; }
        public PatientGetDto Patients { get; set; }
        public HospitalAppointmentGetDto HospitalAppointments { get; set; }
    }
}
