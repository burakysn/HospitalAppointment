using Infrastructure.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IDepatmentRepository : IBaseRepository<Department>
    {
        Task<Department> GetByIdAsync(int departmentId, params string[] includeList);
    }
}
