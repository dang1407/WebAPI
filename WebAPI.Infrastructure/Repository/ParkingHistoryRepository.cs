using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application;
using WebAPI.Domain;

namespace WebAPI.Infrastructure
{
    public class ParkingHistoryRepository : BaseCompanyRepository<ParkingHistory>, IParkingHistoryRepository
    {
        public ParkingHistoryRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<List<ParkingHistory>?> FindParkingHistoryByVehicleOutDateAsync(string? date, string? month, string year)
        {
            string sql = "SELECT * FROM ParkingHistory Where ";
            if(!string.IsNullOrEmpty(date))
            {
                sql += "DAY(VehicleOutDate) = @date AND ";
            }
            if(!string.IsNullOrEmpty(month)) 
            {
                sql += "MONTH(VehicleOutDate) = @month AND ";
            }
            sql += "YEAR(VehicleOutDate) = @year;";
            var param = new DynamicParameters();
            param.Add("date", date);
            param.Add("month", month);
            param.Add("year", year);
            var result = await Uow.Connection.QueryAsync<ParkingHistory>(sql, param);
            return result.ToList();
        }

        public Task<ParkingHistory?> FindParkingHistoryByProperties(string propertyValues, string[] propertyNames)
        {
            throw new NotImplementedException();
        }

        public async Task<ParkingHistory?> FindParkingVehicleAsync(string? licensePlate, Guid companyId  )
        {
            string sql = "SELECT * FROM ParkingHistory where LicensePlate = @licensePlate AND VehicleOutDate IS NULL AND CompanyId = @companyId;";
            var param = new DynamicParameters();
            param.Add("licensePlate", licensePlate);
            param.Add("companyId", companyId);
            var result = await Uow.Connection.QuerySingleOrDefaultAsync(sql, transaction: Uow.Transaction);
            return result;
        }

        public async Task<ParkingHistory> EnterVehicleToGarageAsync(ParkingHistory parkingHistory, Guid companyId)
        {
            throw new NotImplementedException();
            
        }


        /// <summary>
        /// Hàm lấy xe ra khỏi bãi đỗ xe
        /// </summary>
        /// <param name="parkingHistory">Thông tin xe ra</param>
        /// <param name="companyId">Id của công ty</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException">Không tìm thấy xe trong bãi</exception>
        public async Task<ParkingHistory> EnterVehicleOutGarageAsync(ParkingHistory parkingHistory, Guid companyId)
        {
            var existParkingVehicle = await FindParkingVehicleAsync(parkingHistory.LicensePlate, companyId);
            if(existParkingVehicle == null)
            {
                throw new NotFoundException("Không tìm thấy xe trong bãi!");
            } 
            else
            {
                string sql = "Proc_Update_ParkingHistory";
                var param = new DynamicParameters();    
                Type type = parkingHistory.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach(var property in properties)
                {
                    param.Add("p_" + property.Name, property.GetValue(parkingHistory));
                }
                var result = await Uow.Connection.QuerySingleOrDefaultAsync(sql, commandType: System.Data.CommandType.StoredProcedure, transaction: Uow.Transaction);
                return parkingHistory;
            }
        }

        public async Task<List<ParkingHistory>> GetStatisticalParkingHistoryAsync(string year, int vehicle, Guid companyId)
        {
            string sql = "";
            var yearSplit = year.Split(",");
            var param = new DynamicParameters();
            param.Add("CompanyId", companyId.ToString());
            param.Add("year", yearSplit);
            param.Add("Vehicle", vehicle);
            if (yearSplit.Length > 1 )
            {
                if(0<= vehicle && vehicle <= 2)
                {
                    sql = "Select p.* from ParkingHistory p, Parking p1 where p.ParkingId = p1.ParkingId and p1.CompanyId = @CompanyId and YEAR(p.VehicleInDate) in @year and Vehicle = @Vehicle;";
                    
                }
                else
                {
                    sql = "Select p.* from ParkingHistory p, Parking p1 where p.ParkingId = p1.ParkingId and p1.CompanyId = @CompanyId and YEAR(p.VehicleInDate) in @year;";
                }
            }  
            else
            {
                if (0 <= vehicle && vehicle <= 2)
                {
                    sql = "Select p.* from ParkingHistory p, Parking p1 where p.ParkingId = p1.ParkingId and p1.CompanyId = @CompanyId and YEAR(p.VehicleInDate) = @year and Vehicle = @Vehicle;";

                }
                else
                {
                    sql = "Select p.* from ParkingHistory p, Parking p1 where p.ParkingId = p1.ParkingId and p1.CompanyId = @CompanyId and YEAR(p.VehicleInDate) = @year;";
                    //sql = "Select p.* from ParkingHistory p, Parking p1 where p.ParkingId = p1.ParkingId and p1.CompanyId = @CompanyId";
                }
            }
            var result = await Uow.Connection.QueryAsync<ParkingHistory>(sql, param, transaction: Uow.Transaction); 
            return result.ToList();
        }

        
    }
}
