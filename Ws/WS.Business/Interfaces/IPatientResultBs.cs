using Infrastructure.Utilities.ApiResponses;
using WS.Model.Dtos.PatientResult;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface IPatientResultBs 
    {
        Task<ApiResponse<List<PatientResultGetDto>>> GetPatientResultsAsync(params string[] includeList);
        Task<ApiResponse<PatientResultGetDto>> GetByIdAsync(int patientResultId, params string[] includeList);
        Task<ApiResponse<PatientResultGetDto>> InsertAsync(PatientResultPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(PatientResultPutDto entity);
        Task<ApiResponse<NoData>> DeleteAsync(int id);

        Task<ApiResponse<List<PatientResultGetDto>>> GetByPatientIdAsync(int patientId, params string[] includeList);
    }
}
