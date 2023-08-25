using AutoMapper;
using Infrastructure.Model;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Business.CustomExceptions;
using WS.Business.Interfaces;
using WS.DataAccess.Interfaces;
using WS.Model.Dtos.Department;
using WS.Model.Dtos.PatientResult;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class PatientResultBs : IPatientResultBs
    {
        private readonly IPatientResultRepository _repo;
        private readonly IMapper _mapper;

        public PatientResultBs(IPatientResultRepository repo, IMapper mapper)
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
            var patient = await _repo.GetByIdAsync(id);
            if (patient != null)
            {
                patient.IsDeleted = true;
                await _repo.UpdateAsync(patient);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new NotFoundException("Girilen Id'ye göre içerik bulunamadı.");
        }

        public async Task<ApiResponse<PatientResultGetDto>> GetByIdAsync(int patientResultId, params string[] includeList)
        {
            if (patientResultId <= 0)
            {
                throw new BadRequestException("Id değeri 0'dan büyük olmalıdır");
            }
            var patientResult = await _repo.GetByIdAsync(patientResultId, includeList);
            if (patientResult != null)
            {
                var dto = _mapper.Map<PatientResultGetDto>(patientResult);
                return ApiResponse<PatientResultGetDto>.Success(StatusCodes.Status200OK, dto);
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<List<PatientResultGetDto>>> GetByPatientIdAsync(int patientId, params string[] includeList)
        {
            var patientResult = await _repo.GetAllAsync(predicate: k => k.PatientId == patientId, includeList: includeList);
            if (patientResult != null && patientResult.Count > 0)
            {
                var returnList = _mapper.Map<List<PatientResultGetDto>>(patientResult);
                var response = ApiResponse<List<PatientResultGetDto>>.Success(StatusCodes.Status200OK, returnList);
                return response;
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<List<PatientResultGetDto>>> GetPatientResultsAsync(params string[] includeList)
        {
            var patientResult = await _repo.GetAllAsync(predicate: k => !k.IsDeleted, includeList: includeList);
            if (patientResult != null && patientResult.Count > 0)
            {
                var returnList = _mapper.Map<List<PatientResultGetDto>>(patientResult);
                var response = ApiResponse<List<PatientResultGetDto>>.Success(StatusCodes.Status200OK, returnList);
                return response;
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<PatientResultGetDto>> InsertAsync(PatientResultPostDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Kaydedilecek departman bilgisi yollamalısınız");
            }
            if (dto.PatientId <= 0)
            {
                throw new BadRequestException("Hasta Id değeri 0'dan büyük olmalıdır.");
            }
            if (dto.DoctorId <= 0)
            {
                throw new BadRequestException("Doktor Id değeri 0'dan büyük olmalıdır.");
            }
            if (dto.Description == null)
            {
                throw new BadRequestException("Açıklama boş geçilemez.");
            }
            var patientResult = _mapper.Map<PatientResult>(dto);
            var insertedPatientResult = await _repo.InsertAsync(patientResult);
            var retPatRes = _mapper.Map<PatientResultGetDto>(insertedPatientResult);
            return ApiResponse<PatientResultGetDto>.Success(StatusCodes.Status201Created, retPatRes);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(PatientResultPutDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Kaydedilecek Departman bilgisi yollamalısınız");
            }
            if (dto.DoctorId <= 0)
            {
                throw new BadRequestException("Doktor Id değeri 0'dan büyük olmalıdır.");
            }
            if (dto.PatientId <= 0)
            {
                throw new BadRequestException("Hasta Id değeri 0'dan büyük olmalıdır.");
            }
            if (dto.Description == null)
            {
                throw new BadRequestException("Açıklama boş geçilemnez.");
            }
            var patientResult = await _repo.GetByIdAsync(dto.Id);
            if (patientResult != null)
            {
                var patientResultUpdated = _mapper.Map<PatientResult>(dto);
                await _repo.UpdateAsync(patientResultUpdated);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new NotFoundException("Güncellemek istediğiniz içerik bulunamadı.");
        }
    }
}
