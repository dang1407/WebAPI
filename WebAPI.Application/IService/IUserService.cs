using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public interface IUserService : IBaseCompanyService<AccountDTO, RegisterDTO, ForgotPasswordDTO>
    {
        Task<AccountDTO?> FindAccountAsync(AccountDTO loginDTO);
        Task<int> RegisterAsync(RegisterDTO registerDTO, Guid companyId);    
        Task ForgotPassWordAsync(ForgotPasswordDTO forgotPasswordDTO);  
    }
}
