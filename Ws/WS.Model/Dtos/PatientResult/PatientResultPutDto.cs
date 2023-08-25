using Infrastructure.Model;

namespace WS.Model.Dtos.PatientResult
{
    public class PatientResultPutDto : IDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int HospitalAppointmentId { get; set; }
    }
}
