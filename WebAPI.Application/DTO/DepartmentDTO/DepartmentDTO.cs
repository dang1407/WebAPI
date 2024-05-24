using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class DepartmentDTO : BaseDTO
    {
        /// <summary>
        /// Định danh của đơn vị
        /// </summary>
        /// Created by: nkmdang (20/09/2023)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        /// Created by: nkmdang (20/09/2023)
        public string DepartmentName { get; set; } = string.Empty;

        public int EmployeeCount { get; set; }  

    }
}
