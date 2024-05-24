using WebAPI.Domain;
using WebAPI.Domain.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Application
{
    public class EmployeeUpdateDTO : BaseDTO
    {
        #region Properties
        public Guid EmployeeId { get; set; }


        [Required(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "EmployeeCodeNotEmpty")]
        [RegularExpression(@"^NV-[0-9]{6}$", ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "EmployeeCodeNotValid")]
        public string EmployeeCode { get; set; } = string.Empty;


        [Required(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "FullNameNotEmpty")]
        [StringLength(100, ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "FullNameNotEmpty")]
        public string FullName { get; set; } = string.Empty;


        [DateNotInFuture(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "DateOfBirthNotInFuture")]
        [DateOfBirthValidate(18, 70, ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "DateOfBirthNotValid")]
        public DateTimeOffset? DateOfBirth { get; set; }


        public Guid DepartmentId { get; set; }  
        public Gender? Gender { get; set; }


        [Required(ErrorMessage = "TitleId không được để trống")]
        public Guid TitleId { get; set; }

        [RegularExpression(@"^\+?[0-9\s-]*$", ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "MobileNotValid")]
        public string? Mobile { get; set; }

        public string? Address { get; set; }

        [RegularExpression(@"\d*", ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "BankAccountNotValid")]
        public string? BankAccount { get; set; }
        public string? BankBranch { get; set; }

        [StringLength(255, ErrorMessage = "")]
        public string? BankName { get; set; }

        //[RegularExpression(@"^\\d+$", ErrorMessage = "Số CMND chỉ bao gồm các số.")]
        public string? PersonalIdentification { get; set; }

        [DateNotInFuture(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "PICreatedDateNotInFuture")]
        public DateTimeOffset? PICreatedDate { get; set; }

        public string? PICreatedPlace { get; set; }

        [RegularExpression(@"^.+@gmail\.com$", ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "EmailNotValid")]
        public string? Email { get; set; }

        public IFormFile? AvatarFile { get; set; }
        public string? AvatarLink { get; set; }
        #endregion



    }
}
