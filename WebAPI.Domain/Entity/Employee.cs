using WebAPI.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;


namespace WebAPI.Domain
{
    public class Employee : BaseEntity, IEntity
    {

        #region Properties
        public Guid EmployeeId { get; set; }


        [Required(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "EmployeeCodeNotEmpty")]
        [RegularExpression(@"^NV-00[0-9]{4}$", ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "EmployeeCodeNotValid")]
        public string EmployeeCode { get; set; } = string.Empty;


        [Required(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "FullNameNotEmpty")]
        [StringLength(100, ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "FullNameNotEmpty")]
        public string FullName { get; set; } = string.Empty;

        public Guid TitleId { get; set; }
        public Gender? Gender { get; set; }

        [DataType(DataType.DateTime)]
        [DateNotInFuture(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "DateOfBirthNotInFuture")]
        [DateOfBirthValidate(18, 70, ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "DateOfBirthNotValid")]
        public DateTimeOffset? DateOfBirth { get; set; }


        public string? Address { get; set; }

        [Phone(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "MobileNotValid")]
        public string? Mobile { get; set; }

        public string? BankAccount { get; set; }
        public string? BankBranch { get; set; }

        public string? BankName { get; set; }

        public string? PersonalIdentification { get; set; }

        [DateNotInFuture(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "PICreatedDateNotInFuture")]
        public DateTimeOffset? PICreatedDate { get; set; }

        public string? PICreatedPlace { get; set; }

        [RegularExpression(@"^.+@gmail\.com$", ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "EmailNotValid")]
        public string? Email { get; set; }
        public string? AvatarLink { get; set; }
        public string TitleName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string CompanyAddress {  get; set; } = string.Empty; 
        public string CompanyMobile { get; set; } = string.Empty;

        public Guid DepartmentId { get; set; }
        public string CompanyName {  get; set; } = string.Empty;

        #endregion

        #region Override method ToString()
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        #endregion

        #region Implement IEntity

        /// <summary>
        /// Hàm lấy ra Id của Employee
        /// </summary>
        /// <returns>Id của Employee (Guid)</returns>
        /// Created by: nkmdang (19/09/2023)
        public Guid GetId()
        {
            return EmployeeId;
        }


        /// <summary>
        /// Hàm gán giá trị cho Id của Employee
        /// </summary>
        /// <param name="id">Id cần gán cho Employee (Guid)</param>
        /// Created by: nkmdang (19/09/2023)
        public void SetId(Guid id)
        {
            EmployeeId = id;
        }

        #endregion
    }
}
