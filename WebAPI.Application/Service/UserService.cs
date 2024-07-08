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
    public class UserService : BaseCompanyService<Account, AccountDTO, AccountCreateDTO, AccountUpdateDTO>, IUserService
    {
        private readonly IUserRepository _userRepository;   
        public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
        {  
            _userRepository = userRepository;
        }

        public async Task ForgotPassWordAsync(AccountUpdateDTO forgotPasswordDTO)
        {
            //var user = MapUpdateDTOToEntity(forgotPasswordDTO);
            //var result = await _userRepository.UpdateAsync(user);
            //return result;
            throw new NotImplementedException();    
        }

        public async Task<int> RegisterAsync(AccountCreateDTO registerDTO, Guid companyId)
        {
            var user = MapCreateDTOToEntity(registerDTO);
            user.CompanyId = companyId;
            var existResult = await _userRepository.FindAccountAsync(registerDTO.UserName);
            if (existResult != null) 
            {
                throw new ConflictException("User đã tồn tại trong hệ thống");
            }
            var result = await _userRepository.RegisterAsync(user);
            return result;
        }
        public async Task<AccountDTO?> FindAccountAsync(AccountDTO loginDTO)
        {
            // Tìm thông tin tài khoản trong db
            var findAccount = await _userRepository.FindAccountAsync(loginDTO.UserName);

            return MapEntityToDTO(findAccount);
        }

        public async Task<dynamic> GetUserInforAsync(Guid accountId, string role)
        {
            var result = await _userRepository.GetUserInfor(accountId, role);
            return result;
        }
    }
}
