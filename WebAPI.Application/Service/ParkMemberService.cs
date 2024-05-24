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
        public ParkMemberService(IParkMemberRepository parkMemberRepository, IMapper mapper) : base(parkMemberRepository, mapper)
        {

            _parkMemberRepository = parkMemberRepository;   
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

        

        
        
    }
}
