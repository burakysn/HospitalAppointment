using MVC.Areas.Admin.Models;

namespace MVC.Areas.Admin.ViewModels
{
    public class GlobalViewModel
    {
        public List<DoctorItem> Doctors { get; set; }
        public List<DepartmentItem> Departments { get; set; }
        public List<PatientItem> Patients { get; set; }

        public int DoctorCount { get; set; }
        public int DepartmentCount { get; set; }
        public int PatientCount { get; set; }
    }
}
