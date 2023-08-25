using AutoMapper;
using Infrastructure.Model;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;
using WS.Business.CustomExceptions;
using WS.Business.Interfaces;
using WS.DataAccess.Interfaces;
using WS.Model.Dtos.DoctorHospital;
using WS.Model.Dtos.HospitalAppointment;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class HospitalAppointmentBs : IHospitalAppointmentBs
    {
        private readonly IHospitalAppointmentRepository _repo;
        private readonly IMapper _mapper;

        public HospitalAppointmentBs(IHospitalAppointmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ApiResponse<NoData>> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id değeri 0'dan büyük olmalıdır.");
            }
            var hospitalAppointment = await _repo.GetByIdAsync(id);
            if (hospitalAppointment != null)
            {
                hospitalAppointment.IsDeleted = true;
                await _repo.UpdateAsync(hospitalAppointment);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new NotFoundException("Girilen Id'ye göre içerik bulunamadı.");
        }

        public async Task<ApiResponse<HospitalAppointmentGetDto>> GetByIdAsync(int hospitalAppointmentId, params string[] includeList)
        {
            if (hospitalAppointmentId <= 0)
            {
                throw new BadRequestException("Id değeri 0'dan büyük olmalıdır");
            }
            var doctorHospital = await _repo.GetByIdAsync(hospitalAppointmentId, includeList);
            if (doctorHospital != null)
            {
                var dto = _mapper.Map<HospitalAppointmentGetDto>(doctorHospital);
                return ApiResponse<HospitalAppointmentGetDto>.Success(StatusCodes.Status200OK, dto);
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<List<HospitalAppointmentGetDto>>> GetHospitalAppointmentsAsync(params string[] includeList)
        {
            var hospitalAppointment = await _repo.GetAllAsync(predicate: k => !k.IsDeleted, includeList: includeList);
            if (hospitalAppointment != null && hospitalAppointment.Count > 0)
            {
                var returnList = _mapper.Map<List<HospitalAppointmentGetDto>>(hospitalAppointment);
                var response = ApiResponse<List<HospitalAppointmentGetDto>>.Success(StatusCodes.Status200OK, returnList);
                return response;
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<HospitalAppointmentGetDto>> InsertAsync(HospitalAppointmentPostDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Kaydedilecek randevu bilgisi yollamalısınız");
            }
            if (dto.PatientId <= 0)
            {
                throw new BadRequestException("Hasta Id değeri 0'dan büyük olmalıdır.");
            }
            if (dto.DoctorId <= 0)
            {
                throw new BadRequestException("Doktor Id değeri 0'dan büyük olmalıdır.");
            }
            if (dto.Time == null)
            {
                throw new BadRequestException("Zaman değeri boş geçilemez.");
            }
            var hospitalAppointment = _mapper.Map<HospitalAppointment>(dto);
            var insertedHospitalAppointment = await _repo.InsertAsync(hospitalAppointment);
            var retHosAp = _mapper.Map<HospitalAppointmentGetDto>(insertedHospitalAppointment);
            return ApiResponse<HospitalAppointmentGetDto>.Success(StatusCodes.Status201Created, retHosAp);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(HospitalAppointmentPutDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Kaydedilecek Doktor Departman bilgisi yollamalısınız");
            }
            if (dto.PatientId <= 0)
            {
                throw new BadRequestException("Hasta Id değeri 0'dan büyük olmalıdır.");
            }
            if (dto.DoctorId <= 0)
            {
                throw new BadRequestException("Doktor Id değeri 0'dan büyük olmalıdır.");
            }
            if (dto.Time == null)
            {
                throw new BadRequestException("Zaman değeri boş geçilemez.");
            }
            var hospitalAppointment = await _repo.GetByIdAsync(dto.Id);
            if (hospitalAppointment != null)
            {
                var hospitalAppointmentUpdated = _mapper.Map<HospitalAppointment>(dto);
                await _repo.UpdateAsync(hospitalAppointmentUpdated);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new NotFoundException("Güncellemek istediğiniz içerik bulunamadı.");
        }
    }
}
