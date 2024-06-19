using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public interface IBaseCompanyRepository<TEntity>
    {
        /// <summary>
        /// Hàm lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Created by: nkmdang (19/09/2023)
        Task<List<TEntity>> GetFilterAsync(int page, int pageSize, string? property, Guid companyId  );
        
        /// <summary>
        /// Hàm lấy ra số bản ghi thỏa mãn dựa theo tiêu chí client đang xem
        /// </summary>
        /// <param name="property">Thông tin tìm kiếm</param>
        /// <param name="parentId">Id mục tiêu</param>
        /// <returns></returns>
        Task<int> GetNumberRecordsAsync(string? property, Guid companyId  );


        /// <summary>
        /// Hàm tìm kiếm Entity theo Id
        /// </summary>
        /// <param name="id">Định danh của Entity (Guid)</param>
        /// <returns>Thông tin Entity nếu thành công, null nếu thất bại</returns>
        /// Created by: nkmdang (19/09/2023)
        Task<TEntity> FindByIdAsync(Guid id, Guid companyId);

        /// <summary>
        /// Hàm lấy thông tin Entity theo Id
        /// </summary>
        /// <param name="id">Định danh của Entity (Guid)</param>
        /// <returns>Thông tin Entity nếu thành công, null nếu thất bại</returns>
        /// Created by: nkmdang (19/09/2023)
        Task<TEntity> GetByIdAsync(Guid id, Guid companyId);

        /// <summary>
        /// Hàm lấy thông tin nhiều Entity theo Id
        /// </summary>
        /// <param name="ids">Định danh của các Entity (Guid)</param>
        /// <returns>Thông tin các Entity nếu thành công, null nếu thất bại</returns>
        /// Created by: nkmdang (20/09/2023)
        Task<List<TEntity>> GetByListIdAsync(List<Guid> ids, Guid companyId);   

        /// <summary>
        /// Hàm thêm mới một Entity
        /// </summary>
        /// <param name="entity">Instance của Entity</param>
        /// <returns>Thông tin Entity đã thêm thành công</returns>
        /// Created by: nkmdang (20/09/2023)
        abstract Task<TEntity> InsertAsync(TEntity entity, Guid companyId);


        /// <summary>
        /// Hàm thêm mới nhiều Entity
        /// </summary>
        /// <param name="entities">Instances của Entity</param>
        /// <returns>Thông tin các Entity đã thêm thành công</returns>
        /// Created by: nkmdang (20/09/2023)
        abstract Task<List<TEntity>> InsertManyAsync(List<TEntity> entities, Guid companyId);

        /// <summary>
        /// Hàm sửa thông tin một Entity
        /// </summary>
        /// <param name="entity">Instance của Entity</param>
        /// <returns>Thông tin của Entity sau khi đã thay đổi</returns>
        /// Created by: nkmdang (20/09/2023)
        abstract Task<TEntity> UpdateAsync(TEntity entity, Guid companyId);  

        /// <summary>
        /// Hàm xóa thông tin một Entity
        /// </summary>
        /// <param name="id">Định danh Entity</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: nkmdang (20/09/2023)
        Task<int> DeleteAsync(Guid id, Guid companyId);

        /// <summary>
        /// Hàm xóa thông tin nhiều Entity
        /// </summary>
        /// <param name="ids">Danh sách các dịnh danh Entity</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: nkmdang (20/09/2023)
        Task<int> DeleteManyAsync(List<TEntity> entities, Guid companyId);
    }
}
