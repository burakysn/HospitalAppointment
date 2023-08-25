using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Model.Dtos.Department
{
    public class DepartmentPostDto : IDto
    {
        public string Name { get; set; }
        public int HospitalId { get; set; }
    }
}
