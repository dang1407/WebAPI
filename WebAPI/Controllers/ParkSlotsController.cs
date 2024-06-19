using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Application;
using WebAPI.Infrastructure;

namespace WebAPI.Controllers
{
    //[Authorize(Roles = "admin,employee")]
    public class ParkSlotsController : BaseCompanyController<ParkSlotDTO, ParkSlotCreateDTO, ParkSlotUpdateDTO> 
    {
        private readonly IParkSlotService _parkSlotService;
        public ParkSlotsController(IParkSlotService parkSlotService) : base(parkSlotService) 
        {
            _parkSlotService = parkSlotService; 
        }

        [HttpGet]
        [Route("statistical/{parkingId}")] 
        public async Task<IActionResult> GetParkSlotInforAsync(Guid parkingId)
        {
            var result = await _parkSlotService.GetParkSlotByParkingIdAsync(parkingId);
            int bikecycle = 0, mototbike = 0, car = 0;
            foreach (var item in result) 
            { 
                if(item.Vehicle == 0)
                {
                    bikecycle++;    
                } else if(item.Vehicle == 1) 
                {
                    mototbike++;
                } 
                else if(item.Vehicle == 2)
                {
                    car++;  
                }  
            }
            return Ok(new
            {
                ModelData = result,
                Bikecycle = bikecycle,
                Motorbike = mototbike, 
                Car = car    
            });
        }

        [Authorize]
        public override async Task<IActionResult> GetFilterAsync(int page, int pageSize, string? searchProperty)
        {
            Guid companyId = Guid.Parse(HttpContext.User.FindFirstValue("CompanyId"));
            if (companyId == Guid.Empty)
            {
                return BadRequest("Invalid Credential");
            }
            var result = await BaseCompanyService.GetFilterAsync(page, pageSize, searchProperty, companyId);
            var A1 = new List<ParkSlotDTO>();
            var A2 = new List<ParkSlotDTO>();
            var A3 = new List<ParkSlotDTO>();
            var A4 = new List<ParkSlotDTO>();
            var B1 = new List<ParkSlotDTO>();
            var B2 = new List<ParkSlotDTO>();
            var B3 = new List<ParkSlotDTO>();
            var B4 = new List<ParkSlotDTO>();
            var C1 = new List<ParkSlotDTO>();
            var C2 = new List<ParkSlotDTO>();
            var C3 = new List<ParkSlotDTO>();
            var C4 = new List<ParkSlotDTO>();
            foreach (var item in result)
            {
                if (item.ParkSlotCode.StartsWith("A-1"))
                {
                    A1.Add(item);
                }
                else if (item.ParkSlotCode.StartsWith("A-2"))
                {
                    A2.Add(item);
                }
                else if (item.ParkSlotCode.StartsWith("A-3"))
                {
                    A3.Add(item);
                }
                else if (item.ParkSlotCode.StartsWith("A-4"))
                {
                    A4.Add(item);
                }
                else if (item.ParkSlotCode.StartsWith("B-1"))
                {
                    B1.Add(item);
                }
                else if (item.ParkSlotCode.StartsWith("B-2"))
                {
                    B2.Add(item);
                }
                else if (item.ParkSlotCode.StartsWith("B-3"))
                {
                    B3.Add(item);
                }
                else if (item.ParkSlotCode.StartsWith("B-4"))
                {
                    B4.Add(item);
                }
                else if (item.ParkSlotCode.StartsWith("C-1"))
                {
                    C1.Add(item);
                }
                else if (item.ParkSlotCode.StartsWith("C-2"))
                {
                    C2.Add(item);
                }
                else if (item.ParkSlotCode.StartsWith("C-3"))
                {
                    C3.Add(item);
                }
                else if (item.ParkSlotCode.StartsWith("C-4"))
                {
                    C4.Add(item);
                }
            }
            return Ok(new
            {
                A1 = A1,
                A2 = A2,    
                A3 = A3, 
                A4 = A4,
                B1 = B1,    
                B2 = B2,    
                B3 = B3,
                B4 = B4,    
                C1 = C1,    
                C2 = C2,    
                C3 = C3, 
                C4 = C4,    
            });
        }

        public override async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Guid companyId = Guid.Parse(HttpContext.User.FindFirstValue("CompanyId"));
            var result = await _parkSlotService.GetByIdAsync(id, companyId); 
            return Ok(result);    
        }
    }




}
