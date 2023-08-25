using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WS.Model.Entities;

namespace WS.DataAccess.EF.Contexts
{
    public class AppointmentContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("SERVER =.\\SQLEXPRESS; Database = HospitalAppointment; trusted_connection = true;");
        }

        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DoctorHospital> DoctorHospitals { get; set; }
        public DbSet<DoctorDepartment> DoctorDepartments { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<HospitalAppointment> HospitalAppointments { get; set; }
        public DbSet<PatientResult> PatientResults { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
    }
}
