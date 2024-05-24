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
