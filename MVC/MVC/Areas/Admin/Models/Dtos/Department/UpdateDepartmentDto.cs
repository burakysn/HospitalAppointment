namespace MVC.Areas.Admin.Models.Dtos.Department
{
    public class UpdateDepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HospitalId { get; set; }
    }
}
