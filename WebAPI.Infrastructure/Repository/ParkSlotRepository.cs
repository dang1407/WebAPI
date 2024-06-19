using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application;
using WebAPI.Domain;

namespace WebAPI.Infrastructure
{
    public class ParkSlotRepository : BaseCompanyRepository<ParkSlot>, IParkSlotRepository
    {
        public ParkSlotRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<ParkSlot> GetParkSlotById(Guid parkSlotId, Guid companyId)
        {
            string sql = "Select p.* from parkslot p, parking p1 where ParkSlotId = @ParkSlotId and p.ParkingId = p1.ParkingId and p1.CompanyId = @CompanyId;";
            var param = new DynamicParameters();
            param.Add("ParkSlotId", parkSlotId);
            param.Add("CompanyId", companyId);
            var result = await Uow.Connection.QueryAsync<ParkSlot>(sql, param);
            return (ParkSlot)result.ToList()[0];
        }

        public async Task<List<ParkSlot>> GetParkSlotByParkingIdAsync(Guid parkingId)
        {
            string sql = "Select * from ParkSlot where ParkingId = @ParkingId;";
            var param = new DynamicParameters();
            param.Add("ParkingId", parkingId.ToString());   
            var result = await Uow.Connection.QueryAsync<ParkSlot>(sql, param, transaction: Uow.Transaction); 
            return result.ToList(); 
        }

        /// <summary>
        /// Hàm lấy thông tin trạng thái các vị trí để xe theo tầng
        /// </summary>
        /// <param name="floor">Tầng của bãi gửi xe</param>
        /// <returns>Thông tin các vị trí để xe ở tầng cần tìm (List<ParkSlot>)</returns>
        public async Task<List<ParkSlot>> GetParkSlotsByFloorAsync(string floor)
        {
            // Tạo câu lệnh SQL
            string sql = "SELECT * FROM parkslot WHERE Floor = @Floor Order By ParkSlotCode;";

            // Tao Dynamic Param
            var param = new DynamicParameters();
            param.Add("Floor", floor);

            // Truy van
            var result = await Uow.Connection.QueryAsync<ParkSlot>(sql, param);
            return result.ToList();
        }

        public async Task<ParkSlot> GetParkSlotsByLicensePlateAsync(string licensePlate)
        {
            string sql = "SELECT * FROM ParkSlot WHERE LicensePlate = @LicensePlate;";
            var param = new DynamicParameters();
            param.Add("LicensePlate", licensePlate);

            var result = await Uow.Connection.QuerySingleOrDefaultAsync<ParkSlot>(sql, param);
            return result;
        }
    }
}
