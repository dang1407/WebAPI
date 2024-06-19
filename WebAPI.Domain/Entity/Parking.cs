using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public class Parking : BaseEntity, IEntity
    {
        public Guid ParkingId {  get; set; }    
        public string ParkingName { get; set; }
        public Guid CompanyId { get; set; } 
        public int MotorSlot { get; set; }  
        public int CarSlot { get; set; }
        public int BikecycleSlot { get; set; }  

        public Guid GetId()
        {
            return ParkingId;
        }

        public void SetId(Guid Id)
        {
            ParkingId = Id;
        }
    }
}
