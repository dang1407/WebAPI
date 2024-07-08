using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class ParkMemberService : BaseCompanyService<ParkMember, ParkMemberDTO, ParkMemberCreateDTO, ParkMemberUpdateDTO>, IParkMemberService
    {

        private readonly IParkMemberRepository _parkMemberRepository;
        private readonly IUserService _accountService;
        private readonly IPasswordService _passwordService;   
        public ParkMemberService(IParkMemberRepository parkMemberRepository, IUserService accountService, IMapper mapper, IPasswordService passwordService) : base(parkMemberRepository, mapper)
        {

            _parkMemberRepository = parkMemberRepository;   
            _accountService = accountService; 
            _passwordService = passwordService; 
        }

        public Task<byte[]> ExportParkMemberExcelAsync(List<ParkMemberDTO> parkMemberDTOs, int page, int pageSize)
        {
            var parkMembers = parkMemberDTOs.Select(parkMemberDTO => MapDTOToEntity(parkMemberDTO)).ToList();   

            var excelBytes = _parkMemberRepository.ExportParkMembersExcelAsync(parkMembers, page, pageSize);    

            return excelBytes;  
        }

        public async Task<List<ParkMemberDTO>> GetByListIdAsync(List<Guid> ids, Guid companyId)
        {
            var parkMembers = await _parkMemberRepository.GetByListIdAsync(ids, companyId);
            var parkMemberDTOs = parkMembers.Select(parkMember => MapEntityToDTO(parkMember)).ToList(); 
            return parkMemberDTOs;  
        }

        /// <summary>
        /// Hàm lấy ra mã khách hàng gửi xe mới
        /// </summary>
        /// <returns>Mã khách hàng gửi xe mới (string)</returns>
        public async Task<string> GetNewParkMemberCodeAsync(Guid companyId)
        {
            var newParkMemberCode = await _parkMemberRepository.GetNewParkMemberCodeAsync();
            return newParkMemberCode.ToString();
        }

        public async Task<List<ParkMemberDTO>> GetTopParkMemberAsync(string? year, string? month, int limit, Guid companyId)
        {
            var result = await _parkMemberRepository.GetTopParkMemberAsync(year, month, limit, companyId); 
            return result.Select(r =>  MapEntityToDTO(r)).ToList(); 
        }


        /// <summary>
        /// Hàm tạo mới một ParkMember và tạo Account cho ParkMember
        /// </summary>
        /// <param name="createDTO"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Lỗi khi tạo Account</exception>
        public override async Task<ParkMemberDTO> InsertAsync(ParkMemberCreateDTO createDTO, Guid companyId)
        {
            if (string.IsNullOrEmpty(createDTO.ParkMemberCode))
            {
                createDTO.ParkMemberCode = await GetNewParkMemberCodeAsync(companyId);
            }
            var account = new AccountCreateDTO();
            account.UserName = createDTO.UserName;
            account.Password = _passwordService.ComputeSha256Hash(createDTO.Password);
            account.RewritePassword = createDTO.Password;
            account.CompanyId = companyId;
            account.AccountId = Guid.NewGuid();
            account.Role = "parkmember";
            createDTO.AccountId = account.AccountId;    
              
            var newAccount = await _accountService.InsertAsync(account, companyId);
            if (newAccount == null)
            {
                throw new Exception("Lỗi khi tạo Account cho ParkMember");
            }
            try
            {
            var newParkMember = await base.InsertAsync(createDTO, companyId);
            return newParkMember;
            } catch(Exception ex) 
            {
                await _accountService.DeleteAsync(account.AccountId, companyId);
                throw new Exception("Lỗi hệ thống");
            }     
        }



    }
}
