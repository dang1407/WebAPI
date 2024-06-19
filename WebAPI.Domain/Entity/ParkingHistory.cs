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
        public DateTimeOffset? VehicleInDate { get; set; }
        public Guid ParkingId { get; set; } 
        public string? VehicleInImageLink {get; set;}
        public int Vehicle { get; set; } = -1;

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
