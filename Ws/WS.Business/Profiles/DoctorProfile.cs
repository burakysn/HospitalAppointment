using AutoMapper;
using WS.Model.Dtos.Doctor;
using WS.Model.Entities;

namespace WS.Business.Profiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorGetDto>();
            CreateMap<DoctorPostDto, Doctor>().ForMember(dest => dest.Picture, opt => opt.MapFrom(src => Convert.FromBase64String(src.PictureBase64)));
            CreateMap<DoctorPutDto, Doctor>();
        }
    }
}
