using Infrastructure.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IDoctorHospitalRepository : IBaseRepository<DoctorHospital>
    {
        public async Task<DoctorHospital> GetByIdAsync(int doctorHospitalId, params string[] includeList)
        {
            return await GetAsync(dpr => dpr.Id == doctorHospitalId, includeList);
        }
    }
}
