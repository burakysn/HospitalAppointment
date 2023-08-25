using Infrastructure.Utilities.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Department;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface IDepartmentBs
    {
        Task<ApiResponse<List<DepartmentGetDto>>> GetDepartmentsAsync(params string[] includeList);
        Task<ApiResponse<DepartmentGetDto>> GetByIdAsync(int departmentId, params string[] includeList);
        Task<ApiResponse<DepartmentGetDto>> InsertAsync(DepartmentPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(DepartmentPutDto entity);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
    }
}
