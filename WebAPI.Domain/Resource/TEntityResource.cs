using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public class TEntityResource
    {
        public static readonly string TEntityNotFound = "Không tìm thấy tài nguyên.";
        public static readonly string CannotCreateNewTEntity = "Không thể thêm mới tài nguyên.";
        public static readonly string CreateNewTEntitySuccess = "Thêm mới tài nguyên thành công.";
        public static readonly string UpdateTEntitySuccess = "Thay đổi thông tin tài nguyên thành công.";
        public static readonly string DeleteTEntitySuccess = "Xóa tài nguyên thành công.";
        public static readonly string CannotGetTEntitys = "Không thể lấy được thông tin tài nguyên.";
        public static readonly string TEntityCodeExist = "Mã tài nguyên đã tồn tại! Vui lòng nhập mã tài nguyên khác.";
    }
}
