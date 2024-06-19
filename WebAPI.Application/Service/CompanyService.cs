using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class CompanyService : BaseCompanyService<Company, CompanyDTO, CompanyCreateDTO, CompanyUpdateDTO>, ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;   
        public CompanyService(ICompanyRepository baseCompanyRepository, IMapper mapper) : base(baseCompanyRepository, mapper)
        {
            _companyRepository = baseCompanyRepository;
        }


        /// <summary>
        /// Hàm lấy ra toàn bộ thông tin công ty
        /// </summary>
        /// <returns></returns>
        public async Task<List<CompanyDTO>> GetAllAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            var companyDTOs = companies.Select(company => MapEntityToDTO(company)).ToList();    
            return companyDTOs; 
        }

        public async Task<CompanyDTO> GetCompanyByIdAsync(Guid companyId)
        {
            var result = await _companyRepository.GetCompanyByIdAsync(companyId);
            return MapEntityToDTO(result);
        }
    }
}
