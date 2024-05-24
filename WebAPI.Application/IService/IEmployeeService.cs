using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public interface IEmployeeService : IBaseCompanyService<EmployeeDTO, EmployeeCreateDTO, EmployeeUpdateDTO> 
    {
       

       
        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới (string)</returns>
        /// Created by: nkmdang (27/09/2023)
        Task<string> GetNewEmployeeCodeAsync(Guid companyId);

        Task<List<EmployeeDTO>> GetByListIdAsync(List<Guid> ids, Guid companyId);

        

        #region Chức năng xuất file excel
        /// <summary>
        /// Hàm xuất thông tin nhân viên ra excel
        /// </summary>
        /// <param name="employeeDTOs">Thông tin nhân viên</param>
        /// <param name="page">Số thứ tự trang</param>
        /// <param name="pageSize">Số bản ghi trong trang</param>
        /// <returns></returns>
        /// Created by: nkmdang 08/10/2023
        Task<byte[]> ExportEmployeeExcelAsync(List<EmployeeDTO> employeeDTOs, int page, int pageSize);
        #endregion
    }
}
