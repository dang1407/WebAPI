using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class ConfigCreateDTO : BaseDTO
    {
        /// <summary>
        /// Định danh của Config (Guid)
        /// </summary>
        public Guid ConfigId { get; set; }

        /// <summary>
        /// Định danh của DateConfiguration (định dạng ngày tháng năm sẽ hiển thị ra FE)
        /// </summary>
        public Guid DateConfigurationId { get; set; }

        /// <summary>
        /// Chuỗi biểu diễn định dạng DateConfiguration
        /// </summary>
        public string DatePattern { get; set; }
    }
}
