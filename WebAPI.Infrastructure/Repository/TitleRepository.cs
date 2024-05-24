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
    public class TitleRepository : BaseCompanyRepository<Title>, ITitleRepository
    {
        public TitleRepository(IUnitOfWork uow) : base(uow)
        {
        }
        /// <summary>
        /// Hàm kiểm tra TitleId và DepartmentId đầu vào có hợp lệ hay không
        /// </summary>
        /// <param name="titleId"></param>
        /// <param name="departmentId"></param>
        /// <param name="companyId"></param>
        /// <returns>Số Title thỏa mãn với TitleId</returns>
        /// Created by: nkmdang 29/03/2024
        public async Task<int> CheckExistTitleByTitleIdAndDepartmentIdAsync(Guid titleId, Guid departmentId, Guid companyId)
        {
            string sql = "SELECT Count(TitleId) from view_read_titles where TitleId = @titleId and DepartmentId = @departmentId and CompanyId = @companyId";
            var param = new DynamicParameters();
            param.Add("titleId", titleId);
            param.Add("departmentId", departmentId);
            param.Add("companyId", companyId);
            var result = await Uow.Connection.QuerySingleAsync<int>(sql, param, transaction: Uow.Transaction);
            return result;
        }
    }
}
