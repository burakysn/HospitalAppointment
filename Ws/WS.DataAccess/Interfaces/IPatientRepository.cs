using Infrastructure.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<Patient> GetByIdAsync(int patientResultId, params string[] includeList);

        Task<Patient> GetByEmailAndPasswordAsync(string email, string password, params string[] includeList);
    }
}
