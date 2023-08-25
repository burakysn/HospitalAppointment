using MVC.Areas.Admin.Models;

namespace MVC.Areas.Admin.ViewModels
{
    public class HospitalAppointmentViewModel
    {
        public List<HospitalAppointmentItem> HospitalAppointments { get; set; }
        public List<DoctorItem> Doctors { get; set; }
        public List<PatientItem> Patients { get; set; }
    }
}
