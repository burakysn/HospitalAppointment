using AutoMapper;
using Infrastructure.Model;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;
using WS.Business.CustomExceptions;
using WS.Business.Interfaces;
using WS.DataAccess.Interfaces;
using WS.Model.Dtos.Doctor;
using WS.Model.Dtos.DoctorDepartment;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class DoctorBs : IDoctorBs
    {
        private readonly IDoctorRepository _repo;
        private readonly IMapper _mapper;

        public DoctorBs(IDoctorRepository repo, IMapper mapper)
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
            var doctor = await _repo.GetByIdAsync(id);
            if (doctor != null)
            {
                doctor.IsDeleted = true;
                await _repo.UpdateAsync(doctor);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new NotFoundException("Girilen Id'ye göre içerik bulunamadı.");
        }

        public async Task<ApiResponse<DoctorGetDto>> GetByIdAsync(int doctorId, params string[] includeList)
        {
            if (doctorId <= 0)
            {
                throw new BadRequestException("Id değeri 0'dan büyük olmalıdır");
            }
            var doctor = await _repo.GetByIdAsync(doctorId, includeList);
            if (doctor != null)
            {
                var dto = _mapper.Map<DoctorGetDto>(doctor);
                return ApiResponse<DoctorGetDto>.Success(StatusCodes.Status200OK, dto);
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<List<DoctorGetDto>>> GetDoctorsAsync(params string[] includeList)
        {
            var doctor = await _repo.GetAllAsync(predicate: k => !k.IsDeleted, includeList: includeList);
            if (doctor != null && doctor.Count > 0)
            {
                var returnList = _mapper.Map<List<DoctorGetDto>>(doctor);
                var response = ApiResponse<List<DoctorGetDto>>.Success(StatusCodes.Status200OK, returnList);
                return response;
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<DoctorGetDto>> InsertAsync(DoctorPostDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Kaydedilecek Doktor departman bilgisi yollamalısınız");
            }
            if (dto.Name == null)
            {
                throw new BadRequestException("Doktor ismi boş geçilemez.");
            }
            if (dto.Degree == null)
            {
                throw new BadRequestException("Doktor ünvanı boş geçilemez.");
            }
            var doctor = _mapper.Map<Doctor>(dto);
            var insertedDoctor = await _repo.InsertAsync(doctor);
            var retDoc = _mapper.Map<DoctorGetDto>(insertedDoctor);
            return ApiResponse<DoctorGetDto>.Success(StatusCodes.Status201Created, retDoc);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(DoctorPutDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Kaydedilecek Doktor departman bilgisi yollamalısınız");
            }
            if (dto.Name == null)
            {
                throw new BadRequestException("Doktor ismi boş geçilemez.");
            }
            if (dto.Degree == null)
            {
                throw new BadRequestException("Doktor ünvanı boş geçilemez.");
            }
            var doctor = await _repo.GetByIdAsync(dto.Id);
            if (doctor != null)
            {
                var doctorUpdated = _mapper.Map<Doctor>(dto);
                await _repo.UpdateAsync(doctorUpdated);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new NotFoundException("Güncellemek istediğiniz içerik bulunamadı.");
        }
    }
}
