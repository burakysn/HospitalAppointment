using AutoMapper;
using Infrastructure.Model;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;
using WS.Business.CustomExceptions;
using WS.Business.Interfaces;
using WS.DataAccess.Interfaces;
using WS.Model.Dtos.DoctorDepartment;
using WS.Model.Dtos.DoctorHospital;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class DoctorHospitalBs : IDoctorHospitalBs
    {
        private readonly IDoctorHospitalRepository _repo;
        private readonly IMapper _mapper;

        public DoctorHospitalBs(IDoctorHospitalRepository repo, IMapper mapper)
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
            var doctorHospital = await _repo.GetByIdAsync(id);
            if (doctorHospital != null)
            {
                doctorHospital.IsDeleted = true;
                await _repo.UpdateAsync(doctorHospital);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new NotFoundException("Girilen Id'ye göre içerik bulunamadı.");
        }

        public async Task<ApiResponse<DoctorHospitalGetDto>> GetByIdAsync(int doctorHospitalId, params string[] includeList)
        {
            if (doctorHospitalId <= 0)
            {
                throw new BadRequestException("Id değeri 0'dan büyük olmalıdır");
            }
            var doctorHospital = await _repo.GetByIdAsync(doctorHospitalId, includeList);
            if (doctorHospital != null)
            {
                var dto = _mapper.Map<DoctorHospitalGetDto>(doctorHospital);
                return ApiResponse<DoctorHospitalGetDto>.Success(StatusCodes.Status200OK, dto);
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<List<DoctorHospitalGetDto>>> GetDoctorHospitalAsync(params string[] includeList)
        {
            var doctorHospital = await _repo.GetAllAsync(predicate: k => !k.IsDeleted, includeList: includeList);
            if (doctorHospital != null && doctorHospital.Count > 0)
            {
                var returnList = _mapper.Map<List<DoctorHospitalGetDto>>(doctorHospital);
                var response = ApiResponse<List<DoctorHospitalGetDto>>.Success(StatusCodes.Status200OK, returnList);
                return response;
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<DoctorHospitalGetDto>> InsertAsync(DoctorHospitalPostDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Kaydedilecek Doktor departman bilgisi yollamalısınız");
            }
            if (dto.HospitalId <= 0)
            {
                throw new BadRequestException("Hastane Id değeri 0'dan büyük olmalıdır.");
            }
            if (dto.DoctorId <= 0)
            {
                throw new BadRequestException("Doktor Id değeri 0'dan büyük olmalıdır.");
            }
            var doctorHospital = _mapper.Map<DoctorHospital>(dto);
            var insertedDoctorHospital = await _repo.InsertAsync(doctorHospital);
            var retDocHos = _mapper.Map<DoctorHospitalGetDto>(insertedDoctorHospital);
            return ApiResponse<DoctorHospitalGetDto>.Success(StatusCodes.Status201Created, retDocHos);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(DoctorHospitalPutDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Kaydedilecek Doktor Departman bilgisi yollamalısınız");
            }
            if (dto.HospitalId <= 0)
            {
                throw new BadRequestException("Hastane Id değeri 0'dan büyük olmalıdır.");
            }
            if (dto.DoctorId <= 0)
            {
                throw new BadRequestException("Doktor Id değeri 0'dan büyük olmalıdır.");
            }
            var doctorHospital = await _repo.GetByIdAsync(dto.Id);
            if (doctorHospital != null)
            {
                var doctorHospitalUpdated = _mapper.Map<DoctorHospital>(dto);
                await _repo.UpdateAsync(doctorHospitalUpdated);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new NotFoundException("Güncellemek istediğiniz içerik bulunamadı.");
        }
    }
}
