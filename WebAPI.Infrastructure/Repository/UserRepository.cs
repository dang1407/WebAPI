using Dapper;
using System.Net.WebSockets;
using WebAPI.Application;
using WebAPI.Domain;

namespace WebAPI.Infrastructure
{
    public class UserRepository : BaseCompanyRepository<Account>, IUserRepository
    {
        public UserRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<Account?> FindAccountAsync(string username)
        {
            string sql = "Proc_Read_User";
            var param = new DynamicParameters();
            param.Add("p_UserName", username);   
     
            var user = await Uow.Connection.QuerySingleOrDefaultAsync<Account>(sql, param, commandType: System.Data.CommandType.StoredProcedure, transaction: Uow.Transaction);
            return user;
        }

        public async Task<int> RegisterAsync(Account user)
        {
            string sql = "";
            var param = new DynamicParameters();    
            var result = await Uow.Connection.QueryFirstOrDefaultAsync(sql, param, transaction: Uow.Transaction);
            if(result != null)
            {
                return 1;
            } 
            else
            {
                return 0;
            }
        }
    }
}
