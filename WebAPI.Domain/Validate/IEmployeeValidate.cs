using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public interface IEmployeeValidate
    {
        /// <summary>
        /// Hàm kiểm tra nhân viên tồn tại theo Mã nhân viên (EmployeeCode)
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên (string)</param>
        /// <returns></returns>
        /// <exception cref="ConflictException">Nếu tồn tại</exception>
        /// Created by: nkmdang (18/09/2023)
        Task CheckEmployeeExistAsync(Employee employee , Guid companyId);

        /// <summary>
        /// Hàm kiểm tra mã nhân viên mới nhập vào có trùng mã nhân viên của người khác không
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên (string)</param>
        /// <param name="employeeId">Định danh nhân viên (Guid)</param>
        /// <returns>true nếu trùng và false nếu không trùng</returns>
        /// Created by: nkmdang (19/09/2023)
        Task CheckDuplicateEmployeeCodeAsync(string employeeCode, Guid employeeId, Guid companyId);

    }
}
