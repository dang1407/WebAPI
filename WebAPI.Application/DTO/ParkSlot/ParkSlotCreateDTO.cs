using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application   
{
    public class ParkSlotCreateDTO : BaseDTO
    {
        public Guid ParkSlotId { get; set; }
        public int Vehicle { get; set; }
        public string? LicensePlate { get; set; } = string.Empty;
        public string ParkSlotCode { get; set; } = string.Empty;
        public int ParkSlotState { get; set; }
        public Guid ParkingId { get; set; } 
        public DateTimeOffset? VehicleInDate { get; set; }
    }
}
