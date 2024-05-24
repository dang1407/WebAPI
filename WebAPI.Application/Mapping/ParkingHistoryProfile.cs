using AutoMapper;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class ParkingHistoryProfile : Profile
    {
        public ParkingHistoryProfile() 
        {
            CreateMap<ParkingHistoryDTO, ParkingHistory>();   
            CreateMap<ParkingHistoryCreateDTO, ParkingHistory>(); 
            CreateMap<ParkingHistoryUpdateDTO, ParkingHistory>();
            CreateMap<ParkingHistory, ParkSlot>();    
            CreateMap<ParkingHistory, ParkingHistoryDTO>(); 
            CreateMap<ParkingHistoryCreateDTO, ParkSlot>();
            CreateMap<ParkingHistoryUpdateDTO, ParkSlot>();  
            CreateMap<ParkingHistoryDTO, ParkSlotUpdateDTO>();
        }  
    }
}
