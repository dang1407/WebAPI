using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public interface IAccountService : IBaseCompanyService<AccountDTO, AccountCreateDTO, AccountUpdateDTO>
    {
        Task<AccountDTO?> FindAccountAsync(AccountDTO loginDTO);
        Task<int> RegisterAsync(AccountCreateDTO registerDTO, Guid companyId);    
        Task ForgotPassWordAsync(AccountUpdateDTO forgotPasswordDTO);
        Task<dynamic> GetUserInforAsync(Guid accountId, string role);
    }
}
