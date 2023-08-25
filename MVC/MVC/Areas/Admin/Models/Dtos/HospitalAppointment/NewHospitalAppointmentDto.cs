namespace MVC.Areas.Admin.Models.Dtos.HospitalAppointment
{
    public class NewHospitalAppointmentDto
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime Time { get; set; }
    }
}
