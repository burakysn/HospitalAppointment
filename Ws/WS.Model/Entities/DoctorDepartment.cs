using Infrastructure.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace WS.Model.Entities
{
    public class DoctorDepartment : IEntity
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int DoctorId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Department Departments { get; set; }
        public Doctor Doctors { get; set; }
    }
}
