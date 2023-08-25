using Infrastructure.DataAccess.Implementations.EF;
using WS.DataAccess.EF.Contexts;
using WS.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.EF.Repositories
{
    public class HospitalAppointmentRepository : BaseRepository<HospitalAppointment, AppointmentContext>, IHospitalAppointmentRepository
    {
        public async Task<HospitalAppointment> GetByIdAsync(int hospitalAppointmentId, params string[] includeList)
        {
            return await GetAsync(dpr => dpr.Id == hospitalAppointmentId && !dpr.IsDeleted, includeList);
        }
    }
}
