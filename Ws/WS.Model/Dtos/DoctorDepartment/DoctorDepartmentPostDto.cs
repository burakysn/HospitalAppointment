using Infrastructure.Model;

namespace WS.Model.Dtos.DoctorDepartment
{
    public class DoctorDepartmentPostDto : IDto
    {
        public int DepartmentId { get; set; }
        public int DoctorId { get; set; }
    }
}
