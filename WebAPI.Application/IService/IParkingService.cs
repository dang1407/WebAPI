using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public interface IParkingService : IBaseCompanyService<ParkingDTO, ParkingCreateDTO, ParkingUpdateDTO>
    {
        Task<List<ParkingDTO>> GetParkingsByCompanyIdAsync(Guid companyId);
    }
}
