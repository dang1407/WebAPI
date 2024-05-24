using AutoMapper;
using WebAPI.Domain;


namespace WebAPI.Application
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDTO>(); 
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<EmployeeCreateDTO, Employee>();
            CreateMap<EmployeeUpdateDTO, Employee>();   
        }
    }
}
