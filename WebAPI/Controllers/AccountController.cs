using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Application;
using WebAPI.Domain;

namespace WebAPI.Controllers
{
    public class AccountController : BaseCompanyController<AccountDTO, AccountCreateDTO, AccountUpdateDTO>
    {
        private readonly IAccountService _userService; 
        private readonly IEmployeeService _employeeService; 
        private readonly IParkMemberService _parkMemberService; 

        public AccountController(IAccountService userService, IEmployeeService employeeService, IParkMemberService parkMemberService) : base(userService)
        {
            _userService = userService;
            _employeeService = employeeService;
            _parkMemberService = parkMemberService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] AccountCreateDTO createDTO, Guid companyId)
        {
            var result = await _userService.RegisterAsync(createDTO, companyId);  
            return Ok(result);  
        }

        [HttpGet]
        [Route("infor")]
        public async Task<IActionResult> GetUserInforAsync()
        {
            Guid companyId = Guid.Parse(HttpContext.User.FindFirstValue("CompanyId"));
            Guid accountId = Guid.Parse(HttpContext.User.FindFirstValue("AccountId"));
            string role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            if(role == "parkmember")
            {
                var result = await _parkMemberService.GetFilterAsync(1, 1, accountId.ToString(), companyId);
                return Ok(result);
            }
            else if(role == "admin" || role == "employee")
            {
                var result = await _employeeService.GetFilterAsync(1, 1, accountId.ToString(), companyId);
                return Ok(result);

            }
            return Unauthorized();
        }
    }
}
