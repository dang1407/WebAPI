using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebAPI.Application;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ParkMembersController : BaseCompanyController<ParkMemberDTO, ParkMemberCreateDTO, ParkMemberUpdateDTO>
    {
        private IParkMemberService _parkMemberService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IUserService _userService;
        
        public ParkMembersController(IParkMemberService parkMemberService, ICloudinaryService cloudinaryService, IUserService userService) : base(parkMemberService)
        {
            _parkMemberService = parkMemberService;
            _cloudinaryService = cloudinaryService; 
            _userService = userService;
        }

       


        /// <summary>
        /// Hàm lấy mã khách hàng mới bằng mã khách hàng lớn nhất + 1
        /// </summary>
        /// <returns>Mã khách hàng gửi xe mới</returns>
        [HttpGet]
        [Route("NewParkMemberCode/{companyId}")]
        public async Task<IActionResult> GetNewParkMemberCodeAsync( Guid companyId)
        {
            var result = await _parkMemberService.GetNewParkMemberCodeAsync(companyId);  
            return Ok(result);
        }

        
    }
}
