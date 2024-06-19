using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile() 
        {
            CreateMap<Company, CompanyDTO>();
            CreateMap<CompanyDTO, Company>();
            CreateMap<CompanyCreateDTO, Company>();
            CreateMap<CompanyUpdateDTO, Company>();
        } 
    }
}
