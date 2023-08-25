using AutoMapper;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;
using WS.Business.CustomExceptions;
using WS.Business.Interfaces;
using WS.DataAccess.Interfaces;
using WS.Model.Dtos.AdminUser;
using WS.Model.Dtos.User;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class PatientBs : IPatientBs
    {
        private readonly IPatientRepository _repo;
        private readonly IMapper _mapper;

        public PatientBs(IPatientRepository repo, IMapper mapper)
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

        public async Task<ApiResponse<PatientGetDto>> GetByIdAsync(int patientId, params string[] includeList)
        {
            if (patientId <= 0)
            {
                throw new BadRequestException("Id değeri 0'dan büyük olmalıdır");
            }
            var patient = await _repo.GetByIdAsync(patientId, includeList);
            if (patient != null)
            {
                var dto = _mapper.Map<PatientGetDto>(patient);
                return ApiResponse<PatientGetDto>.Success(StatusCodes.Status200OK, dto);
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<List<PatientGetDto>>> GetPatientsAsync(params string[] includeList)
        {
            var patient = await _repo.GetAllAsync(predicate: k => !k.IsDeleted, includeList: includeList);
            if (patient != null && patient.Count > 0)
            {
                var returnList = _mapper.Map<List<PatientGetDto>>(patient);
                var response = ApiResponse<List<PatientGetDto>>.Success(StatusCodes.Status200OK, returnList);
                return response;
            }
            throw new NotFoundException("İçerik Bulunamadı");
        }

        public async Task<ApiResponse<PatientGetDto>> InsertAsync(PatientPostDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Kaydedilecek user bilgisi yollamalısınız");
            }
            dto.Name = dto.Name.Trim();
            if (dto.Name.Length < 3)
            {
                throw new BadRequestException("İsim en az 3 karakter olmalıdır.");
            }
            dto.SurName = dto.SurName.Trim();
            if (dto.SurName.Length < 3)
            {
                throw new BadRequestException("Soyisim en az 3 karakter olmalıdır.");
            }
            if (dto.Email == null)
            {
                throw new BadRequestException("Email boş geçilenmez.");
            }
            if (dto.Password == null)
            {
                throw new BadRequestException("Şifre boş geçilenmez.");
            }
            var patient = _mapper.Map<Patient>(dto);
            var insertedPatient = await _repo.InsertAsync(patient);
            var retPat = _mapper.Map<PatientGetDto>(insertedPatient);
            return ApiResponse<PatientGetDto>.Success(StatusCodes.Status201Created, retPat);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(PatientPutDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Kaydedilecek hastane bilgisi yollamalısınız");
            }
            dto.Name = dto.Name.Trim();
            if (dto.Name.Length < 3)
            {
                throw new BadRequestException("İsim en az 3 karakter olmalıdır.");
            }
            dto.SurName = dto.SurName.Trim();
            if (dto.SurName.Length < 3)
            {
                throw new BadRequestException("Soyisim en az 3 karakter olmalıdır.");
            }
            if (dto.Email == null)
            {
                throw new BadRequestException("Email boş geçilenmez.");
            }
            if (dto.Password == null)
            {
                throw new BadRequestException("Şifre boş geçilenmez.");
            }
            var patient = await _repo.GetByIdAsync(dto.Id);
            if (patient != null)
            {
                var patientUpdated = _mapper.Map<Patient>(dto);
                await _repo.UpdateAsync(patientUpdated);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new NotFoundException("Güncellemek istediğiniz içerik bulunamadı.");
        }
        public async Task<ApiResponse<PatientGetDto>> LogIn(string email, string password, params string[] includeList)
        {
            email = email.Trim();
            if (string.IsNullOrEmpty(email))
            {
                throw new BadRequestException("Email Boş Bırakılamaz.");
            }

            password = password.Trim();
            if (string.IsNullOrEmpty(password))
            {
                throw new BadRequestException("Şifre Boş Bırakılamaz.");
            }

            var adminUser = await _repo.GetByEmailAndPasswordAsync(email, password, includeList);

            if (adminUser != null)
            {
                var dto = _mapper.Map<PatientGetDto>(adminUser);
                return ApiResponse<PatientGetDto>.Success(StatusCodes.Status200OK, dto);
            }
            throw new NotFoundException("İçerik Bulunamadı.");
        }
    }
}
