using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.HospitalAppointment;
using WS.Model.Entities;

namespace WS.Business.Profiles
{
    public class HospitalAppointmentProfile : Profile
    {
        public HospitalAppointmentProfile()
        {
            CreateMap<HospitalAppointment, HospitalAppointmentGetDto>();
            CreateMap<HospitalAppointmentPostDto, HospitalAppointment>();
            CreateMap<HospitalAppointmentPutDto, HospitalAppointment>();
        }
    }
}
