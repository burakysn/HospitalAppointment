using MVC.Areas.Admin.Models;

namespace MVC.Areas.Admin.ViewModels
{
    public class DepartmentViewModel
    {
        public List<DepartmentItem> Deparment { get; set; }
        public List<HospitalItem> Hospital { get; set; }
    }
}
