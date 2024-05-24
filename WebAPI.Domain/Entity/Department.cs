using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public class Department : BaseEntity, IEntity
    {

        /// <summary>
        /// Định danh của đơn vị
        /// </summary>
        public Guid DepartmentId { get; set; }  

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; }

        public int EmployeeCount { get; set; }  

        /// <summary>
        /// Hàm lấy ra Id của Entity
        /// </summary>
        /// <returns>Id của Entity (Guid)</returns>
        /// Created by: nkmdang (19/09/2023)
        public Guid GetId()
        {
            return DepartmentId;
        }


        /// <summary>
        /// Hàm gán giá trị cho Id của Entity
        /// </summary>
        /// <param name="id">Id cần gán cho Entity (Guid)</param>
        /// Created by: nkmdang (19/09/2023)
        public void SetId(Guid id)
        {
            DepartmentId = id; 
        }
    }
}
