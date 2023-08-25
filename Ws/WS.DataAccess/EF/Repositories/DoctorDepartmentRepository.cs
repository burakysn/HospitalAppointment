using Infrastructure.DataAccess.Implementations.EF;
using WS.DataAccess.EF.Contexts;
using WS.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.EF.Repositories
{
    public class DoctorDepartmentRepository : BaseRepository<DoctorDepartment, AppointmentContext>, IDoctorDepartmentRepository
    {
    }
}
