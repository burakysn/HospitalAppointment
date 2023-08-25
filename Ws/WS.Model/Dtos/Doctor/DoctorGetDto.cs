using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Model.Dtos.Doctor
{
    public class DoctorGetDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Degree { get; set; }
        public string DoctorPicture { get; set; }
        public string? PicturePath { get; set; }
        public int DoctorCount { get; set; } = 0;
    }
}
