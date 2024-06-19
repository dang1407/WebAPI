using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
     public interface IUserRepository : IBaseCompanyRepository<Account>  
    {
        Task<Account?> FindAccountAsync(string username);    
        Task<int> RegisterAsync(Account user); 
        Task<dynamic> GetUserInfor(Guid accountId, string role);
    }
}
