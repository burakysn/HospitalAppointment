using Infrastructure.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace WS.Model.Entities
{
    public class PatientResult : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int HospitalAppointmentId { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Doctor Doctors { get; set; }
        public Patient Patients { get; set; }
        public HospitalAppointment HospitalAppointments { get; set; }
    }
}
