using Infrastructure.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace WS.Model.Entities
{
    public class DoctorHospital : IEntity
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int HospitalId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Doctor Doctors { get; set; }
        public Hospital Hospitals { get; set; }
    }
}
