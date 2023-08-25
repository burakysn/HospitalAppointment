using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.DoctorDepartment;
using WS.Model.Dtos.Hospital;
using WS.Model.Entities;

namespace WS.Business.Profiles
{
    public class DoctorDepartmentProfile : Profile
    {
        public DoctorDepartmentProfile()
        {
            CreateMap<DoctorDepartment, DoctorDepartmentGetDto>();
            CreateMap<DoctorDepartmentPostDto, DoctorDepartment>();
            CreateMap<DoctorDepartmentPutDto, DoctorDepartment>();
        }
    }
}
