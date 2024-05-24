using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class ParkingHistoryDTO : BaseDTO
    {
        public Guid ParkingHistoryId { get; set; }
        public string? ParkMemberCode { get; set; } = string.Empty;
        public int Price { get; set; }
        public string LicensePlate { get; set; } = string.Empty;

        public DateTimeOffset? VehicleOutDate { get; set; }

        public int Vehicle { get; set; }
        public string? VehicleInImageLink { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }

    }
}
