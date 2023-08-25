using Infrastructure.DataAccess.Implementations.EF;
using WS.DataAccess.EF.Contexts;
using WS.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.EF.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor, AppointmentContext>, IDoctorRepository
    {
        public async Task<Doctor> GetByIdAsync(int doctorId, params string[] includeList)
        {
            return await GetAsync(dc => dc.Id == doctorId && !dc.IsDeleted, includeList);
        }
    }
}
