using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public interface IConfigRepository : IBaseCompanyRepository<Config>
    {
        ///// <summary>
        ///// Hàm lấy ra định dạng ngày, tháng, năm đang được sử dụng
        ///// </summary>
        ///// <returns>Định dạng ngày tháng năm đang được sử dụng (dd/MM/yyyy hoặc định dạng khác)</returns>
        ///// Created by: nkmdang (07/10/2023)
        //Task UpdateConnc(Config config);

        //Task<Config> GetConfigAsync();
    }
}
