using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    /// <summary>
    /// Lớp biểu diễn các Config của dự án
    /// </summary>
    /// Created by: nkmdang (07/10/2023)
    public class Config : BaseEntity, IEntity
    {
        /// <summary>
        /// Định danh của Config (Guid)
        /// </summary>
        public Guid ConfigId { get; set; }

        /// <summary>
        /// Định danh của DateConfiguration (định dạng ngày tháng năm sẽ hiển thị ra FE)
        /// </summary>
        public Guid? DateConfigurationId { get; set; }

        /// <summary>
        /// Chuỗi biểu diễn định dạng DateConfiguration
        /// </summary>
        /// Created by: nkmdang (07/10/2023)
        public string? DatePattern { get; set; }

        /// <summary>
        /// Hàm ghi đè hàm GetId của IEntity
        /// </summary>
        /// <returns>Id của Config</returns>
        /// Created by: nkmdang (07/10/2023)
        public Guid GetId()
        {
            return ConfigId;    
        }

        /// <summary>
        /// Hàm ghi đè hàm SetId của IEntity
        /// </summary>
        /// <param name="id">id muốn đặt cho Config</param>
        /// Created by: nkmdang (07/10/2023)
        public void SetId(Guid id)
        {
            ConfigId = id;  
        }
    }
}
