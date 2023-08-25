using AutoMapper;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;
using WS.Business.CustomExceptions;
using WS.Business.Interfaces;
using WS.DataAccess.Interfaces;
using WS.Model.Dtos.DoctorDepartment;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class DoctorDepartmentBs : IDoctorDepartmentBs
    {
        private readonly IDoctorDepartmentRepository _repo;
        private readonly IMapper _mapper;

        public DoctorDepartmentBs(IDoctorDepartmentRepository repo, IMapper mapper)
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
            var doctorDepartment = await _repo.GetByIdAsync(id);
            if (doctorDepartment != null)
            {
                doctorDepartment.IsDeleted = true;
                await _repo.UpdateAsync(doctorDepartment);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new NotFoundException("Girilen Id'ye göre içerik bulunamadı.");
        }

        public async Task<ApiResponse<DoctorDepartmentGetDto>> GetByIdAsync(int doctorDepartmentId, params string[] includeList)
        {
            if (doctorDepartmentId <= 0)
            {
                throw new BadRequestException("Id değeri 0'dan büyük olmalıdır");
            }
            var doctorDepartment = await _repo.GetByIdAsync(doctorDepartmentId, includeList);
            if (doctorDepartment != null)
            {
                var dto = _mapper.Map<DoctorDepartmentGetDto>(doctorDepartment);
                return ApiResponse<DoctorDepartmentGetDto>.Success(StatusCodes.Status200OK, dto);
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<List<DoctorDepartmentGetDto>>> GetDoctorDepartmentsAsync(params string[] includeList)
        {
            var doctorDepartment = await _repo.GetAllAsync(predicate: k => !k.IsDeleted, includeList: includeList);
            if (doctorDepartment != null && doctorDepartment.Count > 0)
            {
                var returnList = _mapper.Map<List<DoctorDepartmentGetDto>>(doctorDepartment);
                var response = ApiResponse<List<DoctorDepartmentGetDto>>.Success(StatusCodes.Status200OK, returnList);
                return response;
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<DoctorDepartmentGetDto>> InsertAsync(DoctorDepartmentPostDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Kaydedilecek Doktor departman bilgisi yollamalısınız");
            }
            if (dto.DepartmentId <= 0)
            {
                throw new BadRequestException("Departman Id değeri 0'dan büyük olmalıdır.");
            }
            if (dto.DoctorId <= 0)
            {
                throw new BadRequestException("Doktor Id değeri 0'dan büyük olmalıdır.");
            }
            var doctorDepartment = _mapper.Map<DoctorDepartment>(dto);
            var inserteddoctorDepartment = await _repo.InsertAsync(doctorDepartment);
            var retDocDep = _mapper.Map<DoctorDepartmentGetDto>(inserteddoctorDepartment);
            return ApiResponse<DoctorDepartmentGetDto>.Success(StatusCodes.Status201Created, retDocDep);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(DoctorDepartmentPutDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Kaydedilecek Doktor Departman bilgisi yollamalısınız");
            }
            if (dto.DepartmentId <= 0)
            {
                throw new BadRequestException("Departman Id değeri 0'dan büyük olmalıdır.");
            }
            if (dto.DoctorId <= 0)
            {
                throw new BadRequestException("Doktor Id değeri 0'dan büyük olmalıdır.");
            }
            var doctorDepartment = await _repo.GetByIdAsync(dto.Id);
            if (doctorDepartment != null)
            {
                var doctorDepartmentUpdated = _mapper.Map<DoctorDepartment>(dto);
                await _repo.UpdateAsync(doctorDepartmentUpdated);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new NotFoundException("Güncellemek istediğiniz içerik bulunamadı.");
        }
    }
}
