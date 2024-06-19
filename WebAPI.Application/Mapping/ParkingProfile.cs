using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class ParkingProfile : Profile
    {
        public ParkingProfile() 
        {
            CreateMap<Parking, ParkingDTO>();
            CreateMap<ParkingCreateDTO, Parking>();
            CreateMap<ParkingUpdateDTO, Parking>();
            CreateMap<ParkingDTO, Parking>();
        } 
    }
}
