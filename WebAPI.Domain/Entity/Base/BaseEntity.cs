using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{

    /// <summary>
    /// Lớp abstract chứa các thuộc tính chung của Entity
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTimeOffset? CreatedDate { get; set; }


        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }


        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTimeOffset? ModifiedDate { get; set; }


        /// <summary>
        /// Người sửa
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
