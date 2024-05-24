using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public interface ITitleRepository :  IBaseCompanyRepository<Title>
    {
        /// <summary>
        /// Hàm kiểm tra TitleId và DepartmentId đầu vào có hợp lệ hay không
        /// </summary>
        /// <param name="titleId"></param>
        /// <param name="departmentId"></param>
        /// <param name="companyId"></param>
        /// <returns>Số Title thỏa mãn với TitleId</returns>
        /// Created by: nkmdang 29/03/2024
        public Task<int> CheckExistTitleByTitleIdAndDepartmentIdAsync(Guid titleId, Guid departmentId, Guid companyId);
    }
}
