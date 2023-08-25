using Microsoft.Extensions.DependencyInjection;
using WS.Business.Implementations;
using WS.Business.Interfaces;
using WS.Business.Profiles;
using WS.DataAccess.EF.Repositories;
using WS.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.Business
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AdminUserProfile));
            services.AddAutoMapper(typeof(DepartmentProfile));

            services.AddScoped<IAdminUserBs, AdminUserBs>();
            services.AddScoped<IAdminUserRepository, AdminUserRepository>();

            services.AddScoped<IDepartmentBs, DepartmentBs>();
            services.AddScoped<IDepatmentRepository, DepartmentRepository>();

            services.AddScoped<IHospitalBs, HospitalBs>();
            services.AddScoped<IHospitalRepository, HospitalRepository>();

            services.AddScoped<IDoctorDepartmentBs, DoctorDepartmentBs>();
            services.AddScoped<IDoctorDepartmentRepository, DoctorDepartmentRepository>();

            services.AddScoped<IDoctorHospitalBs, DoctorHospitalBs>();
            services.AddScoped<IDoctorHospitalRepository, DoctorHospitalRepository>();

            services.AddScoped<IHospitalAppointmentBs, HospitalAppointmentBs>();
            services.AddScoped<IHospitalAppointmentRepository, HospitalAppointmentRepository>();

            services.AddScoped<IPatientResultBs, PatientResultBs>();
            services.AddScoped<IPatientResultRepository, PatientResultRepository>();

            services.AddScoped<IPatientBs, PatientBs>();
            services.AddScoped<IPatientRepository, PatientRepository>();

            services.AddScoped<IDoctorBs, DoctorBs>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
        }
    }
}
