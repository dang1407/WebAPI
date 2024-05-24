using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class UserService : BaseCompanyService<Account, AccountDTO, RegisterDTO, ForgotPasswordDTO>, IUserService
    {
        private readonly IUserRepository _userRepository;   
        public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
        {  
            _userRepository = userRepository;
        }

        public async Task ForgotPassWordAsync(ForgotPasswordDTO forgotPasswordDTO)
        {
            //var user = MapUpdateDTOToEntity(forgotPasswordDTO);
            //var result = await _userRepository.UpdateAsync(user);
            //return result;
            throw new NotImplementedException();    
        }

        public async Task<int> RegisterAsync(RegisterDTO registerDTO, Guid companyId)
        {
            var user = MapCreateDTOToEntity(registerDTO);
            user.CompanyId = companyId;
            var result = await _userRepository.RegisterAsync(user);
            return result;
        }
        public async Task<AccountDTO?> FindAccountAsync(AccountDTO loginDTO)
        {
            // Tìm thông tin tài khoản trong db
            var findAccount = await _userRepository.FindAccountAsync(loginDTO.UserName);

            return MapEntityToDTO(findAccount);
        }
    }
}
