using Infrastructure.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
  public interface IAdminUserRepository : IBaseRepository<AdminUser>
  {
    Task<AdminUser> GetByUserNameAndPasswordAsync(string userName,string password, params string[] includeList);
  }
}
