using AutoMapper;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;
using WS.Business.CustomExceptions;
using WS.Business.Interfaces;
using WS.DataAccess.Interfaces;
using WS.Model.Dtos.Department;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class DepartmentBs : IDepartmentBs
    {
        private readonly IDepatmentRepository _repo;
        private readonly IMapper _mapper;

        public DepartmentBs(IDepatmentRepository repo, IMapper mapper)
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
            var department = await _repo.GetByIdAsync(id);
            if (department != null)
            {
                department.IsDeleted = true;
                await _repo.UpdateAsync(department);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new NotFoundException("Girilen Id'ye göre içerik bulunamadı.");
        }

        public async Task<ApiResponse<DepartmentGetDto>> GetByIdAsync(int departmentId, params string[] includeList)
        {
            if (departmentId <= 0)
            {
                throw new BadRequestException("Id değeri 0'dan büyük olmalıdır");
            }
            var department = await _repo.GetByIdAsync(departmentId, includeList);
            if (department != null)
            {
                var dto = _mapper.Map<DepartmentGetDto>(department);
                return ApiResponse<DepartmentGetDto>.Success(StatusCodes.Status200OK, dto);
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<List<DepartmentGetDto>>> GetDepartmentsAsync(params string[] includeList)
        {
            var department = await _repo.GetAllAsync(predicate: k => !k.IsDeleted, includeList: includeList);
            if (department != null && department.Count > 0)
            {
                var returnList = _mapper.Map<List<DepartmentGetDto>>(department);
                var response = ApiResponse<List<DepartmentGetDto>>.Success(StatusCodes.Status200OK, returnList);
                return response;
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<DepartmentGetDto>> InsertAsync(DepartmentPostDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Kaydedilecek departman bilgisi yollamalısınız");
            }
            dto.Name = dto.Name.Trim();
            if (dto.Name.Length < 3)
            {
                throw new BadRequestException("Departman adı en az 3 karakter olmalıdır.");
            }
            if (dto.HospitalId <= 0)
            {
                throw new BadRequestException("Hastane Id değeri 0'dan büyük olmalıdır.");
            }
            var department = _mapper.Map<Department>(dto);
            var inserteddepartment = await _repo.InsertAsync(department);
            var retDep = _mapper.Map<DepartmentGetDto>(inserteddepartment);
            return ApiResponse<DepartmentGetDto>.Success(StatusCodes.Status201Created, retDep);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(DepartmentPutDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Kaydedilecek Departman bilgisi yollamalısınız");
            }
            dto.Name = dto.Name.Trim();
            if (dto.Name.Length < 3)
            {
                throw new BadRequestException("Departman adı en az 3 karakter olmalıdır.");
            }
            if (dto.HospitalId <= 0)
            {
                throw new BadRequestException("Hastane Id değeri 0'dan büyük olmalıdır.");
            }
            var departman = await _repo.GetByIdAsync(dto.Id);
            if (departman != null)
            {
                var departmanUpdated = _mapper.Map<Department>(dto);
                await _repo.UpdateAsync(departmanUpdated);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new NotFoundException("Güncellemek istediğiniz içerik bulunamadı.");
        }
    }
}
