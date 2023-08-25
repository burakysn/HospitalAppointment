namespace MVC.Areas.Admin.Models.Dtos.Hospital
{
    public class UpdateHospitalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Int64 PhoneNumber { get; set; }
        public string HospitalPicture { get; set; }
    }
}
