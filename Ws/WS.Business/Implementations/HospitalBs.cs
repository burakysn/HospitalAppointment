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
using WS.Model.Dtos.Hospital;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class HospitalBs : IHospitalBs
    {
        private readonly IHospitalRepository _repo;
        private readonly IMapper _mapper;

        public HospitalBs(IHospitalRepository repo, IMapper mapper)
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
            var hospital = await _repo.GetByIdAsync(id);
            if (hospital != null)
            {
                hospital.IsDeleted = true;
                await _repo.UpdateAsync(hospital);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new NotFoundException("Girilen Id'ye göre içerik bulunamadı.");
        }

        public async Task<ApiResponse<HospitalGetDto>> GetByIdAsync(int hospitalId, params string[] includeList)
        {
            if (hospitalId <= 0)
            {
                throw new BadRequestException("Id değeri 0'dan büyük olmalıdır");
            }
            var hospital = await _repo.GetByIdAsync(hospitalId, includeList);
            if (hospital != null)
            {
                var dto = _mapper.Map<HospitalGetDto>(hospital);
                return ApiResponse<HospitalGetDto>.Success(StatusCodes.Status200OK, dto);
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<List<HospitalGetDto>>> GetHospitalsAsync(params string[] includeList)
        {
            var hospital = await _repo.GetAllAsync(predicate: k => !k.IsDeleted, includeList: includeList);
            if (hospital != null && hospital.Count > 0)
            {
                var returnList = _mapper.Map<List<HospitalGetDto>>(hospital);
                var response = ApiResponse<List<HospitalGetDto>>.Success(StatusCodes.Status200OK, returnList);
                return response;
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<HospitalGetDto>> InsertAsync(HospitalPostDto dto)
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
            var hospital = _mapper.Map<Hospital>(dto);
            var insertedhospital = await _repo.InsertAsync(hospital);
            var retHos = _mapper.Map<HospitalGetDto>(insertedhospital);
            return ApiResponse<HospitalGetDto>.Success(StatusCodes.Status201Created, retHos);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(HospitalPutDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Kaydedilecek hastane bilgisi yollamalısınız");
            }
            dto.Name = dto.Name.Trim();
            if (dto.Name.Length < 3)
            {
                throw new BadRequestException("Hastane adı en az 3 karakter olmalıdır.");
            }
            var hospital = await _repo.GetByIdAsync(dto.Id);
            if (hospital != null)
            {
                var hospitalUpdated = _mapper.Map<Hospital>(dto);
                await _repo.UpdateAsync(hospitalUpdated);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new NotFoundException("Güncellemek istediğiniz içerik bulunamadı.");
        }
    }
}
