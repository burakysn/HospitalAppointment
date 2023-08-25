namespace MVC.Areas.Admin.Models
{
    public class DepartmentItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HospitalId { get; set; }
        public HospitalItem Hospitals { get; set; }
    }
}
