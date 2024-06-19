using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    /// <summary>
    /// Interface tương tác với Repository của Employee
    /// </summary>
    public interface IEmployeeRepository : IBaseCompanyRepository<Employee>  
    {


        /// <summary>
        /// Hàm lấy thông tin nhân viên theo mã nhân viên (EmployeeCode)
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>Thông tin nhân viên, mã lỗi khi không tìm thấy</returns>
        /// Created by: nkmdang (21/09/2023)
        //Task<Employee> GetEmployeeByCodeAsync(string employeeCode);    

        /// <summary>
        /// Hàm kiểm tra nhân viên tồn tại bằng Mã nhân viên (EmployeeCode)
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên (string)</param>
        /// <returns>Thông tin nhân viên nếu tìm thấy, null nếu không tìm thấy</returns>
        /// Created by: nkmdang (18/09/2023)
        Task<dynamic> IsExistEmployeeAsync(string employeeCode, Guid companyId);
 
        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới (string)</returns>
        /// Created by: nkmdang (27/09/2023)
        Task<string> GetNewEmployeeCodeAsync(Guid companyId);

        
        #region Chức năng xuất file excel
        /// <summary>
        /// Hàm xuất file excel nhân viên
        /// </summary>
        /// <param name="employees">Thông tin nhân viên</param>
        /// <param name="page">Số thứ tự trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <returns>File Excel dạng các byte</returns>
        /// Created by: nkmdang 08/10/2023
        Task<byte[]> ExportEmployeeExcelAsync(List<Employee> employees, int page, int pageSize);
        #endregion
    }
}
