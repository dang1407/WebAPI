using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public interface IParkMemberService : IBaseCompanyService<ParkMemberDTO, ParkMemberCreateDTO, ParkMemberUpdateDTO>
    {


        /// <summary>
        /// Hàm lấy mã khách hàng gửi xe mới
        /// </summary>
        /// <returns>Mã khách hàng gửi xe mới (string)</returns>
        /// Created by: nkmdang (27/09/2023)
        Task<string> GetNewParkMemberCodeAsync( Guid companyId);

        Task<List<ParkMemberDTO>> GetByListIdAsync(List<Guid> ids, Guid companyId);
        Task<List<ParkMemberDTO>> GetTopParkMemberAsync(string? year, string? month, int limit, Guid companyId);

        #region Chức năng xuất file excel
        /// <summary>
        /// Hàm xuất thông tin khách hàng gửi xe ra excel
        /// </summary>
        /// <param name="parkMemberDTOs">Thông tin khách hàng gửi xe</param>
        /// <param name="page">Số thứ tự trang</param>
        /// <param name="pageSize">Số bản ghi trong trang</param>
        /// <returns></returns>
        /// Created by: nkmdang 08/10/2023
        Task<byte[]> ExportParkMemberExcelAsync(List<ParkMemberDTO> parkMemberDTOs, int page, int pageSize);
        #endregion
    }
}
