using Infrastructure.Utilities.ApiResponses;
using WS.Model.Dtos.AdminUser;

namespace WS.Business.Interfaces
{
  public interface IAdminUserBs
  {
    Task<ApiResponse<AdminUserGetDto>> LogIn(string userName,string password, params string[] includeList);
  }
}
