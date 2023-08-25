using Infrastructure.Model;

namespace WS.Model.Dtos.HospitalAppointment
{
    public class HospitalAppointmentPutDto : IDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime Time { get; set; }
    }
}
