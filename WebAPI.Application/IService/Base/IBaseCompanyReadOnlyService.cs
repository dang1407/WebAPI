using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public interface IBaseCompanyReadOnlyService<TDTO>
    {
        /// <summary>
        /// Hàm lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Created by: nkmdang (20/09/2023)
        Task<List<TDTO>> GetFilterAsync(int page, int pageSize, string? property, Guid companyId);


        ///// <summary>
        ///// Hàm tìm kiếm Entity theo Id
        ///// </summary>
        ///// <param name="id">Định danh của Entity (Guid)</param>
        ///// <returns>Thông tin Entity nếu thành công, null nếu thất bại</returns>
        ///// Created by: nkmdang (20/09/2023)
        //Task<TDTO> FindByIdAsync(Guid id, Guid companyId);

        /// <summary>
        /// Hàm lấy thông tin Entity theo Id
        /// </summary>
        /// <param name="id">Định danh của Entity (Guid)</param>
        /// <returns>Thông tin Entity nếu thành công, null nếu thất bại</returns>
        /// Created by: nkmdang (20/09/2023)
        Task<TDTO> GetByIdAsync(Guid id, Guid companyId);

        Task<int> GetNumberRecordsAsync(string? searchProperty, Guid companyId);
    }
}
