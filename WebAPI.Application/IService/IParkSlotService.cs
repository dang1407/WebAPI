using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public interface IParkSlotService : IBaseCompanyService<ParkSlotDTO, ParkSlotCreateDTO, ParkSlotUpdateDTO>
    {
        Task<List<ParkSlotDTO>> GetParkSlotsByFloorAsync(string floor);
        Task<ParkSlotDTO> GetParkSlotByLicensePlateAsync(string licensePlate);
    }
}
