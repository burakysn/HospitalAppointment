namespace MVC.Areas.Admin.Models.Dtos.DoctorHospital
{
    public class UpdateDoctorHospitalDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int HospitalId { get; set; }
    }
}
