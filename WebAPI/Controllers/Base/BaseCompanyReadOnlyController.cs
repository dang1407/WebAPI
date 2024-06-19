using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Application;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    
    public class BaseCompanyReadOnlyController<TDTO> : ControllerBase
    {
        protected readonly IBaseCompanyReadOnlyService<TDTO> BaseCompanyReadOnlyService;
        public BaseCompanyReadOnlyController(IBaseCompanyReadOnlyService<TDTO> baseCompanyReadOnlyService)
        {
            BaseCompanyReadOnlyService = baseCompanyReadOnlyService;
        }

        /// <summary>
        /// Lấy ra toàn bộ dữ liệu về tài nguyên 
        /// </summary>
        /// <returns>EmployeeEntity nếu thành công, mã lỗi nếu thất bại</returns>
        /// Created by: nkmdang (20/09/2023)
        [HttpGet]
        [Route("")]
        [Authorize]
        public virtual async Task<IActionResult> GetFilterAsync(int page, int pageSize, string? searchProperty)
        {
            Guid companyId = Guid.Parse(HttpContext.User.FindFirstValue("CompanyId"));
            if (companyId == Guid.Empty)
            {
                return BadRequest("Invalid Credential");
            }
            int numberRecords = await BaseCompanyReadOnlyService.GetNumberRecordsAsync(searchProperty, companyId);
            
            var result = await BaseCompanyReadOnlyService.GetFilterAsync(page , pageSize, searchProperty, companyId);
            return Ok(new {
                ModelData = result,
                NumberRecords = numberRecords
            });
        }

        /// <summary>
        /// Lấy ra thông tin tài nguyên theo id (char(36))
        /// </summary>
        /// <param name="id">Tham số nhận vào từ route</param>
        /// <returns>Thông tin tài nguyên nếu thành công, mã lỗi nếu thất bại</returns>
        /// Created by: nkmdang (20/09/2023)
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public virtual async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Guid companyId = Guid.Parse(HttpContext.User.FindFirstValue("CompanyId"));
            if(companyId == Guid.Empty) 
            {
                return BadRequest("Invalid Credential");
            }
            var result = await BaseCompanyReadOnlyService.GetFilterAsync(1,1,id.ToString(), companyId);
            if(result.Count > 0)
            {
                return Ok(result);
            }
            return BadRequest("Không tìm thấy tài nguyên");

        }
    }
}
