using AutoMapper;
using WS.Model.Dtos.Hospital;
using WS.Model.Entities;

namespace WS.Business.Profiles
{
    public class HospitalProfile : Profile
    {
        public HospitalProfile()
        {
            CreateMap<Hospital, HospitalGetDto>();
            CreateMap<HospitalPostDto, Hospital>().ForMember(dest => dest.Picture, opt => opt.MapFrom(src => Convert.FromBase64String(src.PictureBase64)));
            CreateMap<HospitalPutDto, Hospital>();
        }
    }
}
