using Infrastructure.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IHospitalRepository : IBaseRepository<Hospital>
    {
        Task<Hospital> GetByIdAsync(int hospitalId, params string[] includeList);
    }
}
