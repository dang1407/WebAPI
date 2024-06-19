using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public interface ICompanyRepository : IBaseCompanyRepository<Company>
    {
        public Task<List<Company>> GetAllAsync();
        Task<Company> GetCompanyByIdAsync(Guid companyId);
    }
}
