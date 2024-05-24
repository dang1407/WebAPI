using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public class ParkSlot : BaseEntity, IEntity
    {
        public Guid ParkSlotId { get; set; }
        public string ParkSlotCode { get; set; } = string.Empty;

        public int ParkSlotState { get; set; }
        public string Floor { get; set; } = string.Empty;

        public int Vehicle {  get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public DateTimeOffset? VehicleInDate { get; set; }
        public Guid GetId()
        {
            return ParkSlotId;
        }

        public void SetId(Guid id)
        {
            ParkSlotId = id;
        }
    }
}
