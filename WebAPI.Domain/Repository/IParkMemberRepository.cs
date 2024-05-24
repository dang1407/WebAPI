using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public interface IParkMemberRepository : IBaseCompanyRepository<ParkMember>
    {

        /// <summary>
        /// Hàm kiểm tra khách hàng gửi xe tồn tại bằng Mã khách hàng gửi xe (ParkMemberCode)
        /// </summary>
        /// <param name="parkMemberCode">Mã khách hàng gửi xe (string)</param>
        /// <returns>Thông tin khách hàng gửi xe nếu tìm thấy, null nếu không tìm thấy</returns>
        /// Created by: nkmdang (18/09/2023)
        Task<dynamic> IsExistParkMemberAsync(string parkMemberCode);

        

        /// <summary>
        /// Hàm lấy mã khách hàng gửi xe mới
        /// </summary>
        /// <returns>Mã khách hàng gửi xe mới (string)</returns>
        /// Created by: nkmdang (27/09/2023)
        Task<string> GetNewParkMemberCodeAsync();




        #region Chức năng xuất file excel
        /// <summary>
        /// Hàm xuất file excel khách hàng gửi xe
        /// </summary>
        /// <param name="parkMembers">Thông tin khách hàng gửi xe</param>
        /// <param name="page">Số thứ tự trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <returns>File Excel dạng các byte</returns>
        /// Created by: nkmdang 08/10/2023
        Task<byte[]> ExportParkMembersExcelAsync(List<ParkMember> parkMembers, int page, int pageSize);
        #endregion
    }
}
