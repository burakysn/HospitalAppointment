using Infrastructure.Model;

namespace WS.Model.Dtos.DoctorDepartment
{
    public class DoctorDepartmentPutDto : IDto
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int DoctorId { get; set; }
    }
}
