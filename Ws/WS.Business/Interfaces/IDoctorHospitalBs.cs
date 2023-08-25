using Infrastructure.Utilities.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Department;
using WS.Model.Dtos.DoctorHospital;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface IDoctorHospitalBs
    {
        Task<ApiResponse<List<DoctorHospitalGetDto>>> GetDoctorHospitalAsync(params string[] includeList);
        Task<ApiResponse<DoctorHospitalGetDto>> GetByIdAsync(int doctorHospitalId, params string[] includeList);
        Task<ApiResponse<DoctorHospitalGetDto>> InsertAsync(DoctorHospitalPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(DoctorHospitalPutDto entity);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
    }
}
