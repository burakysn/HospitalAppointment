namespace MVC.Areas.Admin.Models
{
    public class HospitalItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Int64 PhoneNumber { get; set; }
        public string HospitalPicture { get; set; }
        public string? PicturePath { get; set; }
    }
}
