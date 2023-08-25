using Infrastructure.Utilities.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Department;
using WS.Model.Dtos.HospitalAppointment;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface IHospitalAppointmentBs
    {
        Task<ApiResponse<List<HospitalAppointmentGetDto>>> GetHospitalAppointmentsAsync(params string[] includeList);
        Task<ApiResponse<HospitalAppointmentGetDto>> GetByIdAsync(int hospitalAppointmentId, params string[] includeList);
        Task<ApiResponse<HospitalAppointmentGetDto>> InsertAsync(HospitalAppointmentPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(HospitalAppointmentPutDto entity);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
    }
}
