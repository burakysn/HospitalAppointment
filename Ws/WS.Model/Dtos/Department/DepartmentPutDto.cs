using Infrastructure.Model;

namespace WS.Model.Dtos.Department
{
    public class DepartmentPutDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HospitalId { get; set; }
    }
}
