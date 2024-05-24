using Dapper;
using WebAPI.Application;
using WebAPI.Domain;
using WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Infrastructure
{
    public class DepartmentRepository : BaseCompanyRepository<Department> ,IDepartmentRepository
    {
        public DepartmentRepository(IUnitOfWork uow) : base(uow)
        {
        }

        /// <summary>
        /// Hàm lấy thông tin Department theo DepartmentName
        /// </summary>
        /// <param name="departmentName">Tên đơn vị (string)</param>
        /// <returns>Thông tin đơn vị (Department)</returns>
        /// Created by: nkmdang (21/09/2023)
        public async Task<Department> GetDepartmentByNameAsync(string departmentName)
        {
            string sql = DepartmentSQL.GetDepartmentByNameSQL(departmentName);

            var result = await Uow.Connection.QueryFirstOrDefaultAsync<Department>(sql); 
            
            return result;  
        }
    }
}
