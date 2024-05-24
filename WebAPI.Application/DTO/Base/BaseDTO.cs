using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application {
    public class BaseDTO
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTimeOffset? CreatedDate { get; set; }


        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }


        /// <summary>
        /// Ngày sửa
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTimeOffset? ModifiedDate { get; set; }


        /// <summary>
        /// Người sửa
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
