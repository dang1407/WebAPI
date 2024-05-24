using AutoMapper;
using WebAPI.Application.DTO;
using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class LeaveDaysRequestProfile : Profile
    {
        public LeaveDaysRequestProfile() 
        {
            CreateMap<LeaveDaysRequestDTO, LeaveDaysRequest>();
            CreateMap<LeaveDaysRequest, LeaveDaysRequestDTO>();
            CreateMap<LeaveDaysRequestCreateDTO, LeaveDaysRequest>();
            CreateMap<LeaveDaysRequestUpdateDTO, LeaveDaysRequest>();
        }
    }
}
