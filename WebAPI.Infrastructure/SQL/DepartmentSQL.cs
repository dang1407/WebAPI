namespace WebAPI
{
    public class DepartmentSQL
    {
        /// <summary>
        /// Hàm tạo câu lệnh SQL lấy department theo DepartmentName
        /// </summary>
        /// <param name="departmentName">Tên đơn vị</param>
        /// <returns>Câu lệnh SQL</returns>
        /// Created by: nkmdang (21/09/2023)
        public static string GetDepartmentByNameSQL(string departmentName)
        {
            string sql = $"SELECT * FROM Department WHERE DepartmentName = '{departmentName}'";
            return sql;
        }


        /// <summary>
        /// Tạo câu lệnh SQL thêm mới Đơn vị
        /// </summary>
        /// <returns></returns>
        /// Created by: nkmdang (25/09/2023)
        public static string CreateDepartmentSQL()
        {
            string sql = "";
            return sql;
        }


        /// <summary>
        /// Tạo câu lệnh SQL sửa Đơn vị
        /// </summary>
        /// <returns></returns>
        /// Created by: nkmdang (25/09/2023)
        public static string UpdateDepartmentSQL()
        {
            string sql = "";
            return sql;
        }

        /// <summary>
        /// Hàm lấy câu lệnh SQL lấy ra thông tin các đơn vị
        /// </summary>
        /// <param name="page">Số trang</param>
        /// <param name="pageSize">Số bản ghi trong trang</param>
        /// <param name="departmentProperty">Thuộc tính</param>
        /// <returns></returns>
        public static string GetDepartmentFilterSQL(int page, int pageSize, string? departmentProperty) 
        {
            int firstRecordIndex = page * pageSize;
            string sql = $"SELECT d.*, COUNT(e.EmployeeId) AS EmployeeCount FROM Department d LEFT JOIN Employee e ON e.DepartmentId = d.DepartmentId GROUP BY d.DepartmentId, d.DepartmentName LIMIT {firstRecordIndex}, {pageSize}; ";
            return sql;
        }
    }
}
