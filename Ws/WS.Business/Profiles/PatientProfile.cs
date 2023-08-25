using AutoMapper;
using WS.Model.Dtos.User;
using WS.Model.Entities;

namespace WS.Business.Profiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientGetDto>();
            CreateMap<PatientPostDto, Patient>();
            CreateMap<PatientPutDto, Patient>();
        }
    }
}
