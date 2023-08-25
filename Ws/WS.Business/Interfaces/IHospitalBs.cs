using Infrastructure.Utilities.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Department;
using WS.Model.Dtos.Hospital;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface IHospitalBs
    {
        Task<ApiResponse<List<HospitalGetDto>>> GetHospitalsAsync(params string[] includeList);
        Task<ApiResponse<HospitalGetDto>> GetByIdAsync(int departmentId, params string[] includeList);
        Task<ApiResponse<HospitalGetDto>> InsertAsync(HospitalPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(HospitalPutDto entity);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
    }
}
