using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{

    /// <summary>
    /// Interface đại diện cho tất cả các Entity
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Hàm lấy ra Id của Entity
        /// </summary>
        /// <returns>Id của Entity (Guid)</returns>
        /// Created by: nkmdang (19/09/2023)
        public Guid GetId();

        /// <summary>
        /// Hàm gán giá trị cho Id của Entity
        /// </summary>
        /// <param name="id">Id cần gán cho Entity (Guid)</param>
        /// Created by: nkmdang (19/09/2023)
        public void SetId(Guid id);
    }
}
