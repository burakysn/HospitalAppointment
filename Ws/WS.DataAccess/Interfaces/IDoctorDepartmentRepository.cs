using Infrastructure.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IDoctorDepartmentRepository : IBaseRepository<DoctorDepartment>
    {
        public async Task<DoctorDepartment> GetByIdAsync(int doctorDepartmentId, params string[] includeList)
        {
            return await GetAsync(dpr => dpr.Id == doctorDepartmentId, includeList);
        }
    }
}
