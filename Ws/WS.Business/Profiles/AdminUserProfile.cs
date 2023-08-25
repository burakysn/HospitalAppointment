using AutoMapper;
using WS.Model.Dtos.AdminUser;
using WS.Model.Entities;

namespace WS.Business.Profiles
{
  public class AdminUserProfile : Profile
  {
    public AdminUserProfile()
    {
      CreateMap<AdminUser, AdminUserGetDto>();
    }
  }
}
