using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application.DTO.DepartmentDTO
{
    public class DepartmentCreateDTO : BaseDTO  
    {
        /// <summary>
        /// Tên đơn vị
        /// </summary>
        /// Created by: nkmdang (20/09/2023)
        public string DepartmentName { get; set; } = string.Empty;
    }
}
