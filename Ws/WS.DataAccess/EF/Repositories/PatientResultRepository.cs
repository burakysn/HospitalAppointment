using Infrastructure.DataAccess.Implementations.EF;
using WS.DataAccess.EF.Contexts;
using WS.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.EF.Repositories
{
    public class PatientResultRepository : BaseRepository<PatientResult, AppointmentContext>, IPatientResultRepository
    {
        public async Task<PatientResult> GetByIdAsync(int patientResultId, params string[] includeList)
        {
            return await GetAsync(ptr => ptr.Id == patientResultId && !ptr.IsDeleted, includeList);

        }
    }
}
