using AutoMapper;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class ParkSlotProfile : Profile
    {
        public ParkSlotProfile() 
        {
            CreateMap<ParkSlot, ParkSlotDTO>();    
            CreateMap<ParkSlotCreateDTO, ParkSlot>();
            CreateMap<ParkSlotUpdateDTO, ParkSlot>();
            CreateMap<ParkSlotDTO, ParkSlot>(); 
        }    
    }
}
