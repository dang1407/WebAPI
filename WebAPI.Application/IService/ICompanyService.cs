using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public interface ICompanyService : IBaseCompanyService<CompanyDTO, CompanyCreateDTO, CompanyUpdateDTO>
    {
        public Task<List<CompanyDTO>> GetAllAsync();
        Task<CompanyDTO> GetCompanyByIdAsync(Guid companyId);
    }
}
