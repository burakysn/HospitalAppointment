namespace MVC.Areas.Admin.Models.Dtos.DoctorDepartment
{
    public class UpdateDoctorDepartmentDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int DepartmentId { get; set; }
    }
}
