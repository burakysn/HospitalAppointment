using Infrastructure.DataAccess.Implementations.EF;
using WS.DataAccess.EF.Contexts;
using WS.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.EF.Repositories
{
  public class AdminUserRepository : BaseRepository<AdminUser, AppointmentContext>, IAdminUserRepository
  {
    public async Task<AdminUser> GetByUserNameAndPasswordAsync(string userName, string password,params string[] includeList)
    {
      return await GetAsync(x => x.UserName == userName && x.Password == password && !x.IsDeleted,includeList);
    }
  }
}
