using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.DoctorHospital;
using WS.Model.Entities;

namespace WS.Business.Profiles
{
    public class DoctorHospitalProfile : Profile
    {
        public DoctorHospitalProfile()
        {
            CreateMap<DoctorHospital, DoctorHospitalGetDto>();
            CreateMap<DoctorHospitalPostDto, DoctorHospital>();
            CreateMap<DoctorHospitalPutDto, DoctorHospital>();
        }
    }
}
