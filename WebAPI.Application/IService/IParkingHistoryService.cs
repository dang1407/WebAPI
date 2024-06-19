using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public interface IParkingHistoryService : IBaseCompanyService<ParkingHistoryDTO, ParkingHistoryCreateDTO, ParkingHistoryUpdateDTO>
    {

        Task<ParkingHistoryDTO> FindParkingVehicleAsync(string? licensePlate, Guid companyId);

        Task<ParkingHistoryDTO> EnterVehicleToGarageAsync(ParkingHistoryCreateDTO parkingHistoryCreateDTO, Guid companyId); 
        Task<ParkingHistoryDTO> EnterVehicleOutGarageAsync( ParkingHistoryCreateDTO parkingHistoryCreateDTO, Guid companyId);
        Task<List<ParkingHistoryDTO>> GetParkingHistoryStatistical(string year, int vehicle, Guid companyId);  
    }
}
