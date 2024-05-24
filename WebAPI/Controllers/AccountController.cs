using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application;
using WebAPI.Domain;

namespace WebAPI.Controllers
{
    public class AccountController : BaseCompanyController<AccountDTO, RegisterDTO, ForgotPasswordDTO>
    {
        private readonly IUserService _userService; 
        public AccountController(IUserService userService) : base(userService) {
            _userService = userService; 
        }

        [HttpPost]
        [Route("{companyId}")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO createDTO, Guid companyid)
        {
            var result = await _userService.InsertAsync(createDTO, companyid);  
            return Ok(result);  
        }
    }
}
