using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class ParkSlotDTO
    {
        public Guid ParkSlotId { get; set; }
        
        [Required(ErrorMessage = "Mã vị trí gửi xe không được để trống.")]
        [RegularExpression(@"[A-K]-[1-4]{1}-([1-9]|10)", ErrorMessage = "Mã Vị trí phải có định dạng A-1-1.")] 
        public string ParkSlotCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Trạng thái vị trí gửi xe không được để trống.")]
        public int ParkSlotState { get; set; }

        [Required(ErrorMessage = "Tầng bãi gửi xe không được để trống.")]
        public string Floor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Loại phương tiện không được để trống.")]
        public int Vehicle { get; set; }
        public string? LicensePlate { get; set; } = string.Empty;
        public DateTimeOffset? VehicleInDate { get; set; }
    }
}
