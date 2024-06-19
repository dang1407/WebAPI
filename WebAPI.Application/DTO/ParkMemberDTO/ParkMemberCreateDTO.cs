using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class ParkMemberCreateDTO : BaseDTO
    {
        public Guid ParkMemberId { get; set; }

        [Required(ErrorMessage = "Username không để trống")]
        public string UserName { get; set; }  = string.Empty;

        [Required(ErrorMessage = "Password không để trống")]
        public string Password { get; set; } = string.Empty;

        public string Role = "parkmember";

        [Required(ErrorMessage = "Mã khách hàng gửi xe không được để trống.")]
        [RegularExpression(@"PMB-00[0-9]{4}", ErrorMessage = "Mã khách hàng gửi xe phải có định dạng PMB-00abcd với a, b, c, d là các chữ số.")]
        public string ParkMemberCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ tên khách hàng không được để trống.")]
        [MaxLength(255, ErrorMessage = "Họ tên không thể quá 255 kí tự.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số căn cước công dân không được để trống.")]
        [MaxLength (20, ErrorMessage = "Số căn cước công dân chỉ có 12 kí tự. Vui lòng kiểm tra lại.")]
        public string PersonalIdentification { get; set; } = string.Empty;

        [DateOfBirthValidate(18, 150, ErrorMessage = "Khách hàng phải có độ tuổi từ đủ 18 tuổi trở lên.")]
        public DateTimeOffset? DateOfBirth { get; set; }

        [MaxLength(255, ErrorMessage = "Địa chỉ quá dài.")]
        public string? Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Biển số xe không được để trống.")]
        [MaxLength(20, ErrorMessage = "Biển số xe không có định dạng như vậy. Vui lòng kiểm tra lại.")]
        public string LicensePlate { get; set; } = string.Empty;
        public string? AvatarLink { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;

        public string? Mobile { get; set; } = string.Empty;
        public Gender? Gender { get; set; }
    }
}
