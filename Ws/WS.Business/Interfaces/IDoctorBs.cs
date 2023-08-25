using Infrastructure.Utilities.ApiResponses;
using WS.Model.Dtos.Doctor;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface IDoctorBs
    {
        Task<ApiResponse<List<DoctorGetDto>>> GetDoctorsAsync(params string[] includeList);
        Task<ApiResponse<DoctorGetDto>> GetByIdAsync(int doctorId, params string[] includeList);
        Task<ApiResponse<DoctorGetDto>> InsertAsync(DoctorPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(DoctorPutDto entity);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
    }
}
