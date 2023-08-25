using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.PatientResult;
using WS.Model.Entities;

namespace WS.Business.Profiles
{
    public class PatientResultProfile : Profile
    {
        public PatientResultProfile()
        {
            CreateMap<PatientResult, PatientResultGetDto>();
            CreateMap<PatientResultPostDto, PatientResult>();
            CreateMap<PatientResultPutDto, PatientResult>();
        }
    }
}
