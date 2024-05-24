using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public interface IParkSlotRepository : IBaseCompanyRepository<ParkSlot>    
    {
        Task<List<ParkSlot>> GetParkSlotsByFloorAsync(string floor);
        Task<ParkSlot> GetParkSlotsByLicensePlateAsync(string licensePlate);

    }
}
