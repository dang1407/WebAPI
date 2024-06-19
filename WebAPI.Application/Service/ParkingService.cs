using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class ParkingService : BaseCompanyService<Parking, ParkingDTO, ParkingCreateDTO, ParkingUpdateDTO>, IParkingService
    {
        private readonly IParkingRepository _parkingRepository; 
        public ParkingService(IParkingRepository parkingRepository, IMapper mapper) : base(parkingRepository, mapper) 
        {
            _parkingRepository = parkingRepository; 
        }

        public async Task<List<ParkingDTO>> GetParkingsByCompanyIdAsync(Guid companyId)
        {
            var result = await _parkingRepository.GetParkingsByCompanyIdAsync(companyId);
            return result.Select(x => MapEntityToDTO(x)).ToList();
        }
    }
}
