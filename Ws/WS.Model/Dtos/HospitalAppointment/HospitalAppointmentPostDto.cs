using Infrastructure.Model;

namespace WS.Model.Dtos.HospitalAppointment
{
    public class HospitalAppointmentPostDto : IDto
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime Time { get; set; }
    }
}
