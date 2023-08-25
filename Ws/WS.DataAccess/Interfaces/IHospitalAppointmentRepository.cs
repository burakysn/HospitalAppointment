using Infrastructure.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IHospitalAppointmentRepository : IBaseRepository<HospitalAppointment>
    {
        Task<HospitalAppointment> GetByIdAsync(int hospitalAppointmentId, params string[] includeList);
    }
}
