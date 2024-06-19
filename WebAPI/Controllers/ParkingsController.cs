using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application;

namespace WebAPI.Controllers
{
    public class ParkingsController : BaseCompanyController<ParkingDTO, ParkingCreateDTO, ParkingUpdateDTO>
    {
        public ParkingsController(IParkingService parkingService): base(parkingService) { }
        
    }
}
