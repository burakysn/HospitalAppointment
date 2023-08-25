using Infrastructure.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace WS.Model.Entities
{
    public class HospitalAppointment : IEntity
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime Time { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Doctor Doctors { get; set; }
        public Patient Patients { get; set; }
    }
}
