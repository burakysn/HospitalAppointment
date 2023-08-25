using Infrastructure.Utilities.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Department;
using WS.Model.Dtos.DoctorDepartment;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface IDoctorDepartmentBs
    {
        Task<ApiResponse<List<DoctorDepartmentGetDto>>> GetDoctorDepartmentsAsync(params string[] includeList);
        Task<ApiResponse<DoctorDepartmentGetDto>> GetByIdAsync(int departmentId, params string[] includeList);
        Task<ApiResponse<DoctorDepartmentGetDto>> InsertAsync(DoctorDepartmentPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(DoctorDepartmentPutDto entity);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
    }
}
