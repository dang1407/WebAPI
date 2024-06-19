using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Application;
using WebAPI.Domain;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ParkingHistoryController : BaseCompanyController<ParkingHistoryDTO, ParkingHistoryCreateDTO, ParkingHistoryUpdateDTO>
    {
        private readonly IParkingHistoryService _parkingHistoryService;
        public ParkingHistoryController(IParkingHistoryService parkingHistoryService) : base(parkingHistoryService)
        {
            _parkingHistoryService = parkingHistoryService; 
        }

        [HttpGet]
        [Route("statistical")]
        public async Task<IActionResult> GetParkingHistoryStatisticalAsync(string year, int? vehicle)
        {
            if (vehicle == null)
            {
                vehicle = -1;
            }
            Guid companyId = Guid.Parse(HttpContext.User.FindFirstValue("CompanyId"));
            var result = await _parkingHistoryService.GetParkingHistoryStatistical(year, (int) vehicle, companyId);
            var yearSplit = year.Split(",");
            if (year.Split(",").Length < 2)
            {
                int[] prices = new int[12] {0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0, 0};
                foreach(var p in result)
                {
                    prices[((DateTimeOffset)p.VehicleInDate).Month - 1] += p.Price;    
                }
                return Ok(new
                {
                    Prices = prices
                });
            }
            else
            {
                var finalResult = new List<object>();
                for(int i = 0; i < yearSplit.Length; i++)
                {
                    int[] prices = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    foreach (var p in result)
                    {
                        if(((DateTimeOffset)p.VehicleInDate).Year == Int32.Parse(yearSplit[i]))
                        {

                            prices[((DateTimeOffset)p.VehicleInDate).Month - 1] += p.Price;
                        }
                    }
                    finalResult.Add(new 
                    {
                        Prices = prices,
                        Year = yearSplit[i]
                    });
                }
                return Ok(finalResult);
            } 
        }


    }
}
