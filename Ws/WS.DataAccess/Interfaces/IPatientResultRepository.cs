using Infrastructure.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IPatientResultRepository : IBaseRepository<PatientResult>
    {
        Task<PatientResult> GetByIdAsync(int patientResultId, params string[] includeList);
    }
}
