using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Model.Dtos.Doctor
{
    public class DoctorPostDto : IDto
    {
        public string Name { get; set; }
        public string Degree { get; set; }
        public string? PicturePath { get; set; }
        public string? PictureBase64 { get; set; }
    }
}
