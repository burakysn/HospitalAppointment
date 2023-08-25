using Infrastructure.Model;
using WS.Model.Dtos.Hospital;

namespace WS.Model.Dtos.Department
{
    public class DepartmentGetDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HospitalId { get; set; }
        public HospitalGetDto Hospitals { get; set; }
        public int DepartmentCount { get; set; } = 0;
    }
}
