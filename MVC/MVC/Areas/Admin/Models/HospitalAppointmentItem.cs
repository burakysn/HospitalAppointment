namespace MVC.Areas.Admin.Models
{
    public class HospitalAppointmentItem
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime Time { get; set; }
        public DoctorItem Doctors { get; set; }
        public PatientItem Patients { get; set; }
    }
}
