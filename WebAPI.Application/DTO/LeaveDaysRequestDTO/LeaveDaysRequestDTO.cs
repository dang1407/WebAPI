using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application.DTO
{
    public class LeaveDaysRequestDTO : BaseDTO
    {
        #region Properties
        public Guid LeaveDaysRequestId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }    
        public string FullName { get; set; }
        public string PositionName { get; set; }
        public string DepartmentName { get; set; }
        public Guid DepartmentId { get; set; }
        public string AvatarLink { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset ToDate { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
        public int SalaryRate { get; set; }
        public string ApproveBy { get; set; }
        public string SubstituteBy { get; set; }
        public string LeaveEmployeesId { get; set; }
        public string RelateEmployeesId { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        #endregion



    }
}
