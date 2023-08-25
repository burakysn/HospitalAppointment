namespace MVC.Areas.Admin.Models
{
    public class DoctorItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Degree { get; set; }
        public string DoctorPicture { get; set; }
        public string? PicturePath { get; set; }
    }
}
