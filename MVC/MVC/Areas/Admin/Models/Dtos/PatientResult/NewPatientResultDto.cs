namespace MVC.Areas.Admin.Models.Dtos.PatientResult
{
    public class NewPatientResultDto
    {
        public string Description { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int HospitalAppointmentId { get; set; }
        public DoctorItem Doctors { get; set; }
        public PatientResultItem Patients { get; set; }
    }
}
