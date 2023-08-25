using Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IDoctorRepository : IBaseRepository<Doctor>
    {
        Task<Doctor> GetByIdAsync(int doctorId, params string[] includeList);
    }
}
