using AutoMapper;
using WS.Model.Dtos.Department;
using WS.Model.Entities;

namespace WS.Business.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentGetDto>();
            CreateMap<DepartmentPostDto, Department>();
            CreateMap<DepartmentPutDto, Department>();
        }
    }
}
