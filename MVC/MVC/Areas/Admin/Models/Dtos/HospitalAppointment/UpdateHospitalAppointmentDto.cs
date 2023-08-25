namespace MVC.Areas.Admin.Models.Dtos.HospitalAppointment
{
    public class UpdateHospitalAppointmentDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime Time { get; set; }
    }
}
