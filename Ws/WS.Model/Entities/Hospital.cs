using Infrastructure.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace WS.Model.Entities
{
    public class Hospital : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public Int64? PhoneNumber { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? PicturePath { get; set; }
        public byte[]? Picture { get; set; }

        [NotMapped]
        public string Base64Picture
        {
            get
            {
                if (Picture != null)
                {
                    var base64Str = string.Empty;
                    using (var ms = new MemoryStream())
                    {

                        int offset = 0;
                        ms.Write(Picture, offset, Picture.Length - offset);
                        var bmp = new System.Drawing.Bitmap(ms);
                        using (var jpegms = new MemoryStream())
                        {
                            bmp.Save(jpegms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            base64Str = Convert.ToBase64String(jpegms.ToArray());
                        }


                    }
                    return base64Str;

                }


                return string.Empty;
            }
        }
    }
}
