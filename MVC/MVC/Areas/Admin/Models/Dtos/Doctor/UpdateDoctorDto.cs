namespace MVC.Areas.Admin.Models.Dtos.Doctor
{
    public class UpdateDoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Degree { get; set; }
        public string DoctorPicture { get; set; }
    }
}
