using AutoMapper;
using WebAPI.Domain;


namespace WebAPI.Application
{
    public class ConfigProfile : Profile
    {
        public ConfigProfile()
        {
            CreateMap<Config, ConfigDTO>();
            CreateMap<ConfigDTO, Config>();
            CreateMap<ConfigCreateDTO, Config>();
            CreateMap<ConfigUpdateDTO, Config>();
        }

    }
}
