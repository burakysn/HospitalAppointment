using MVC.Areas.Admin.Models;

namespace MVC.Areas.Admin.ViewModels
{
    public class DoctorHospitalViewModel
    {
        public List<DoctorHospitalItem> DoctorHospitals { get; set; }
        public List<DoctorItem> Doctors { get; set; }
        public List<HospitalItem> Hospitals { get; set; }
    }
}
