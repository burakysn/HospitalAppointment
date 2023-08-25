using MVC.Areas.Admin.Models;

namespace MVC.Areas.Admin.ViewModels
{
    public class DoctorDepartmentViewModel
    {
        public List<DoctorDepartmentItem> DoctorDepartments { get; set; }
        public List<DoctorItem> Doctors { get; set; }
        public List<DepartmentItem> Departments { get; set; }
    }
}
