using Infrastructure.DataAccess.Implementations.EF;
using WS.DataAccess.EF.Contexts;
using WS.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.EF.Repositories
{
    public class DepartmentRepository : BaseRepository<Department, AppointmentContext>, IDepatmentRepository
    {
        public async Task<Department> GetByIdAsync(int departmentId, params string[] includeList)
        {
            return await GetAsync(dpr => dpr.Id == departmentId && !dpr.IsDeleted, includeList);
        }
    }
}
