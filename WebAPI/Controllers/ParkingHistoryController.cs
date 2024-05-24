using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application;
using WebAPI.Domain;

namespace WebAPI.Controllers
{

    public class ParkingHistoryController : BaseCompanyController<ParkingHistoryDTO, ParkingHistoryCreateDTO, ParkingHistoryUpdateDTO>
    {
        public ParkingHistoryController(IParkingHistoryService parkingHistoryService) : base(parkingHistoryService)
        {

        }
    }
}
