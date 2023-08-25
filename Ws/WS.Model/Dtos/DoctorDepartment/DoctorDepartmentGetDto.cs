using Infrastructure.Model;
using WS.Model.Dtos.Department;
using WS.Model.Dtos.Doctor;
using WS.Model.Dtos.User;

namespace WS.Model.Dtos.DoctorDepartment
{
    public class DoctorDepartmentGetDto : IDto
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int DoctorId { get; set; }
        public DepartmentGetDto Departments { get; set; }
        public DoctorGetDto Doctors { get; set; }
    }
}
