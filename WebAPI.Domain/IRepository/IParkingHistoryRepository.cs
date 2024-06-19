using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public interface IParkingHistoryRepository : IBaseCompanyRepository<ParkingHistory>
    {

        Task<ParkingHistory?> FindParkingVehicleAsync(string? licensePlate, Guid companyId);
        Task<ParkingHistory> EnterVehicleToGarageAsync(ParkingHistory parkingHistory, Guid companyId);
        Task<ParkingHistory> EnterVehicleOutGarageAsync(ParkingHistory parkingHistory, Guid companyId);

        Task<List<ParkingHistory>> GetStatisticalParkingHistoryAsync(string year, int vehicle, Guid companyId);

    }
}
