namespace MVC.Areas.Admin.Models
{
    public class DoctorDepartmentItem
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int DepartmentId { get; set; }
        public DoctorItem Doctors { get; set; }
        public DepartmentItem Departments { get; set; }
    }
}
