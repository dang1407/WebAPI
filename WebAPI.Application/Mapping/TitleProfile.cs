using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application.Mapping
{
    public class TitleProfile : Profile
    {
        public TitleProfile() 
        {
            CreateMap<TitleCreatedDTO, Title>();
            CreateMap<TitleUpdateDTO, Title>();
            CreateMap<TitleDTO, Title>();   
            CreateMap<Title, TitleDTO>();
        }   
    }
}
