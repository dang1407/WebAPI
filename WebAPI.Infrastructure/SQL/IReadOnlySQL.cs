using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Infrastructure
{
    public interface IReadOnlySQL<TEntity> where TEntity : IEntity
    {
        public Type EntityType { get; set; }   
        public string Name { get; set; }    
        string GetAllSQL();

        /// <summary>
        /// Hàm tạo câu lệnh SQL lấy một Entity theo id
        /// </summary>
        /// <param name="id">id của Entity</param>
        /// <returns>Câu lệnh SQL</returns>
        string GetByIdSQL(Guid id);

        /// <summary>
        /// Hàm tạo câu lệnh SQL lấy nhiều theo Id
        /// </summary>
        /// <param name="ids">Danh sách id để tìm kiếm Entity theo</param>
        /// <returns>Câu lệnh SQL tìm kiếm nhiều theo ids</returns>
        /// Created by: nkmdang (08/10/2023)
        string GetByListIdSQL(List<Guid> ids);
    }
}
