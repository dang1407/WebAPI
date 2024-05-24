using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class ParkMemberProfile : Profile
    {
        public ParkMemberProfile() 
        {
            CreateMap<ParkMember, ParkMemberDTO>();
            CreateMap<ParkMemberDTO, ParkMember>();
            CreateMap<ParkMemberCreateDTO, ParkMember>();
            CreateMap<ParkMemberUpdateDTO, ParkMember>();

        }  
    }
}
