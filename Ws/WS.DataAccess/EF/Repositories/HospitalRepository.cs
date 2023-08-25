using Infrastructure.DataAccess.Implementations.EF;
using WS.DataAccess.EF.Contexts;
using WS.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.EF.Repositories
{
    public class HospitalRepository : BaseRepository<Hospital, AppointmentContext>, IHospitalRepository
    {
        public async Task<Hospital> GetByIdAsync(int hospitalId, params string[] includeList)
        {
            return await GetAsync(hs => hs.Id == hospitalId && !hs.IsDeleted, includeList);
        }
    }
}
