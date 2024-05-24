using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Infrastructure
{
    public class ParkingHistorySQL
    {
        public static string CreateParkingHistorySQL()
        {
            string sql = "INSERT INTO ParkingHistory (ParkingHistoryId, ParkMemberCode, LicensePlate, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, VehicleOutDate, Price, Vehicle, VehicleInImageLink) VALUES (@ParkingHistoryId, @ParkMemberCode, @LicensePlate, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy, @VehicleOutDate, @Price, @Vehicle, @VehicleInImageLink);";
            return sql;
        }

        public static string UpdateParkingHistorySQL() 
        {
            string sql = "UPDATE ParkingHistory SET VehicleOutDate = @VehicleOutDate, Price = @Price WHERE LicensePlate = @LicensePlate;";
            return sql; 
        }
    }
}
