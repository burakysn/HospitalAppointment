using MVC.Areas.Admin.Models;

namespace MVC.Areas.Admin.ViewModels
{
    public class PatientResultViewModel
    {
        public List<PatientResultItem> PatientResult { get; set; }
        public List<DoctorItem> Doctors { get; set; }
        public List<PatientItem> Patients { get; set; }
        public List<HospitalAppointmentItem> HospitalAppointments { get; set; }
    }
}
