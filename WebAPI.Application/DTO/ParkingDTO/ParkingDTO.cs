using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class ParkingDTO : BaseDTO
    {
        public Guid ParkingId { get; set; }
        public string ParkingName { get; set; }
        public Guid CompanyId { get; set; }
        public int MotorSlot { get; set; }
        public int CarSlot { get; set; }
        public int BikecycleSlot { get; set; }  
    }
}
