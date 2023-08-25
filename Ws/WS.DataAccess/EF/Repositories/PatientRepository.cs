using Infrastructure.DataAccess.Implementations.EF;
using WS.DataAccess.EF.Contexts;
using WS.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.EF.Repositories
{
    public class PatientRepository : BaseRepository<Patient, AppointmentContext>, IPatientRepository
    {
        public async Task<Patient> GetByIdAsync(int userId, params string[] includeList)
        {
            return await GetAsync(usr => usr.Id == userId && !usr.IsDeleted, includeList);
        }

        public async Task<Patient> GetByEmailAndPasswordAsync(string email, string password, params string[] includeList)
        {
            return await GetAsync(x => x.Email == email && x.Password == password && !x.IsDeleted, includeList);
        }
    }
}
