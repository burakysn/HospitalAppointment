using Infrastructure.Model;
using WS.Model.Dtos.Department;

namespace WS.Model.Dtos.Hospital
{
    public class HospitalGetDto : IDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public Int64? PhoneNumber { get; set; }
        public string HospitalPicture { get; set; }
        public string? PicturePath { get; set; }
        public List<DepartmentGetDto> Departments { get; set; }
    }
}
