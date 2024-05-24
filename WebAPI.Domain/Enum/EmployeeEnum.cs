using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public enum EmployeeEnum
    {
        /// <summary>
        /// Không tìm thấy nhân viên
        /// </summary>
        EmployeeNotFoundErrorCode = 404,

        /// <summary>
        /// Mã nhân viên bị trùng
        /// </summary>
        EmployeeCodeExistErrorCode = 409
    }
}
