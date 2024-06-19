using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class ParkingHistoryCreateDTO : BaseDTO
    {
        public Guid ParkingHistoryId { get; set; }
        public string? ParkMemberCode { get; set; } = string.Empty;
        public int Price { get; set; }
        public string? LicensePlate { get; set; } = string.Empty;

        public DateTimeOffset? VehicleOutDate { get; set; }
        public DateTimeOffset VehicleInDate { get; set; }   
        public int Vehicle { get; set; }
        public Guid ParkingId { get; set; }

    }
}
