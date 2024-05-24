using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public interface IBaseService<TDTO, TCreateDTO, TUpdateDTO> : IBaseReadOnlyService<TDTO>
    {
        /// <summary>
        /// Hàm thêm mới một TDTO
        /// </summary>
        /// <param name="createDTO">Instance của TDTO</param>
        /// <returns>Thông tin TDTO đã thêm thành công</returns>
        /// Created by: nkmdang (20/09/2023)
        Task<TDTO> InsertAsync(TCreateDTO createDTO);


        /// <summary>
        /// Hàm thêm mới nhiều TDTO
        /// </summary>
        /// <param name="createDTOs">Instances của TDTO</param>
        /// <returns>Thông tin các TDTO đã thêm thành công</returns>
        /// Created by: nkmdang (20/09/2023)
        Task<List<TDTO>> InsertManyAsync(List<TCreateDTO> createDTOs);

        /// <summary>
        /// Hàm sửa thông tin một TDTO
        /// </summary>
        /// <param name="updateDTO">Instance của TDTO</param>
        /// <returns>Thông tin của TDTO sau khi đã thay đổi</returns>
        /// Created by: nkmdang (20/09/2023)
        Task<TDTO> UpdateAsync(Guid id, TUpdateDTO updateDTO);

        /// <summary>
        /// Hàm xóa thông tin một TDTO
        /// </summary>
        /// <param name="id">Định danh TDTO</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: nkmdang (20/09/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Hàm xóa thông tin nhiều TDTO
        /// </summary>
        /// <param name="ids">Danh sách các dịnh danh TDTO</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: nkmdang (20/09/2023)
        Task<int> DeleteManyAsync(List<Guid> ids);
    }
}
