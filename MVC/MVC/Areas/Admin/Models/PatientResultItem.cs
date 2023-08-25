namespace MVC.Areas.Admin.Models
{
    public class PatientResultItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int HospitalAppointmentId { get; set; }
        public DoctorItem Doctors { get; set; }
        public PatientItem Patients { get; set; }
        public HospitalAppointmentItem HospitalAppointments { get; set; }
    }
}
