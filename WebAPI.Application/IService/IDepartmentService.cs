using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application 
{ 
    public interface IDepartmentService : IBaseCompanyReadOnlyService<DepartmentDTO>
    {
        /// <summary>
        /// Hàm lấy thông tin Department theo DepartmentName
        /// </summary>
        /// <param name="departmentName">Tên đơn vị (string)</param>
        /// <returns>Thông tin đơn vị (Department)</returns>
        /// Created by: nkmdang (21/09/2023)
        Task<DepartmentDTO> GetDepartmentByNameAsync(string departmentName);
    }
}
