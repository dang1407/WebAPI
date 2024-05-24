using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Domain;


namespace WebAPI.Application
{
    public class EmployeeDTO : BaseDTO
    {
        #region Properties
        public Guid EmployeeId { get; set; }


        [Required(ErrorMessage = "EmployeeCode is required.")]
        [RegularExpression(@"^NV-[0-9]{6}$", ErrorMessage = "Mã nhân viên phải có định dạng NV-00abcd với a,b,c,d là các chữ số tự nhiên.")]
        public string EmployeeCode { get; set; } = string.Empty;    


        [Required(ErrorMessage = "FullName is required.")]
        [StringLength(100, ErrorMessage = "Full name must be less than 100 characters.")]
        public string FullName { get; set; } = string.Empty;

        public DateTimeOffset? DateOfBirth { get; set; }

        public Gender? Gender { get; set; }

        [Required]
        public Guid TitleId { get; set; }

        public string? Mobile { get; set; }

        public string? Address { get; set; }

        public string? BankAccount { get; set; }
        public string? BankBranch { get; set; }

        public string? BankName { get; set; }

        public string? PersonalIdentification { get; set; }


        public DateTimeOffset? PICreatedDate { get; set; }

        public string? PICreatedPlace { get; set; }

        public string? Email { get; set; }
        public string? AvatarLink { get; set; }
        public string TitleName { get; set; } = string.Empty;
        [Required]
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
        public string CompanyMobile { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;
        #endregion


    }
}
