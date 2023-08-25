namespace MVC.Areas.Admin.Models
{
    public class DoctorHospitalItem
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int HospitalId { get; set; }
        public DoctorItem Doctors { get; set; }
        public HospitalItem Hospitals { get; set; }
    }
}
