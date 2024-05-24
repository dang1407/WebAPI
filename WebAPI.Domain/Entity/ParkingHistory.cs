using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public class ParkingHistory : BaseEntity, IEntity
    {
        public Guid ParkingHistoryId { get; set; }
        public string ParkMemberCode { get; set; } = string.Empty;
        public int Price { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string ParkSlotCode { get; set; } = string.Empty;
        public int ParkSlotState { get; set; }
        public DateTimeOffset? VehicleOutDate { get; set; }

        public string? VehicleInImageLink {get; set;}
        public int Vehicle { get; set; }

        public string? FullName { get; set; }   
        public string? Address { get; set; }    
        public string? Mobile { get; set; } 

        public string? Floor {  get; set; } 
        public Guid GetId()
        {
            return ParkingHistoryId;
        }

        public void SetId(Guid id)
        {
            ParkingHistoryId = id;
        }
    }
}
