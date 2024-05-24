using AutoMapper;
using WebAPI.Domain;


namespace WebAPI.Application
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDTO>(); 
            CreateMap<DepartmentDTO, Department>();  
        }
    }
}
