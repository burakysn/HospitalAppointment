using Infrastructure.Utilities.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.AdminUser;
using WS.Model.Dtos.User;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface IPatientBs
    {
        Task<ApiResponse<List<PatientGetDto>>> GetPatientsAsync(params string[] includeList);
        Task<ApiResponse<PatientGetDto>> GetByIdAsync(int patientId, params string[] includeList);
        Task<ApiResponse<PatientGetDto>> InsertAsync(PatientPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(PatientPutDto entity);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<PatientGetDto>> LogIn(string userName, string password, params string[] includeList);
    }
}
