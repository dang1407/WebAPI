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
    public class ParkingRepository : BaseCompanyRepository<Parking>, IParkingRepository
    {
        public ParkingRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<List<Parking>> GetParkingsByCompanyIdAsync(Guid companyId)
        {
            string sql = "Select * from Parking where CompanyId = @CompanyId;";
            var param = new DynamicParameters();
            param.Add("CompanyId", companyId.ToString());
            var result = await Uow.Connection.QueryAsync<Parking>(sql, param, transaction: Uow.Transaction);
            return result.ToList();
        }
    }
}
