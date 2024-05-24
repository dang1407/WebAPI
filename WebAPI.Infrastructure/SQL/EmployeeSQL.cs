namespace WebAPI
{
    public class EmployeeSQL
    {
        /// <summary>
        /// Tạo câu lệnh SQL lấy toàn bộ thông tin nhân viên trong bảng Employee
        /// </summary>
        /// <returns>sql (string)</returns>
        /// Created By: nkmdang (11/09/2023)
        public static string GetAllEmployeesSQL()
        {
            var sql = "CALL Proc_Read_GetAllEmployees()";
            //var sql = "SELECT * FROM Employee";
            return sql;
        }

        /// <summary>
        /// Tạo câu lệnh SQL lấy thông tin nhân viên theo Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>sql (string)</returns>
        /// Created By: nkmdang (11/09/2023)
        public static string GetEmployeeByIdSQL(Guid employeeId)
        {
            //var sql = $"SELECT * FROM Employee WHERE EmployeeId = '{employeeId}'";
            string sql = $"CALL Proc_Read_EmployeeById('{employeeId}')";
            return sql;
        }

        /// <summary>
        /// Tạo câu lệnh SQL lấy thông tin nhân viên theo phân trang
        /// </summary>
        /// <param name="page">Số thứ tự trang (int)</param>
        /// <param name="pageSize">Số nhân viên trên trang (int)</param>
        /// <returns>Câu lệnh SQL lấy thông tin nhân viên theo phân trang</returns>
        /// Created By: nkmdang (13/09/2023)
        public static string GetEmployeesPaginationSQL(int page, int pageSize)
        {
            string sql = $"CALL Proc_Read_GetEmployeesFilter('{page}', '{pageSize}')";
            return sql;
        }

        /// <summary>
        /// Tạo câu lệnh SQL lấy thông tin nhân viên theo Mã nhân viên
        /// Cách sử dụng: await connection.ExecuteAsync(sql, employeeFormData);
        /// </summary>
        /// <returns>sql (string)</returns>
        /// Created By: nkmdang (11/09/2023)
        public static string GetEmployeeByEmployeeCodeSQL()
        {
            //string sql = $"SELECT * FROM Employee WHERE EmployeeCode = @EmployeeCode";
            string sql = "CALL Proc_Read_GetEmployeeByEmployeeCode(@EmployeeCode)";
            return sql;
        }


        /// <summary>
        /// Tạo câu lệnh SQL thay đổi thông tin nhân viên theo Id
        /// </summary>
        /// <returns>sql (string)</returns>
        /// Created By: nkmdang (11/09/2023)
        public static string UpdateEmployeeByIdSQL()
        {
            //string sql = $"UPDATE Employee SET EmployeeCode = '{employeeFormData.EmployeeCode}', FullName = '{employeeFormData.FullName}', DateOfBirth = '{employeeFormData.DateOfBirth}', Gender = '{employeeFormData.Gender}' WHERE EmployeeId = '{employeeId}'";
            //Console.WriteLine(sql);
            //string sql = $"UPDATE Employee SET EmployeeCode = @EmployeeCode, FullName = @FullName, DateOfBirth = @DateOfBirth, Gender = @Gender WHERE EmployeeId = '{employeeId}'";
            //string execProcedure = "EXEC Proc_Employee_AddNewEmployee @EmployeeCode, @FullName, @DateOfBirth, @Gender";
            string sql = $"CALL Proc_Update_UpdateOneEmployeeById(@EmployeeId, @EmployeeCode,  @FullName, @DepartmentId,  @Gender,  @DateOfBirth,  @Address,  @BankName,  @BankBranch,  @BankAccount,  @Email,  @Mobile,  @LandLinePhone,  @PersonalIdentification,  @PICreatedDate,  @PICreatedPlace,  @PositionName,  @CreatedDate,  @CreatedBy, @ModifiedDate, @ModifiedBy, @AvatarLink)";
            return sql;
        }

        
        //public static string CreateOneEmployeeSQL()
        //{
        //    var sql = $"INSERT INTO Employee (FullName, DateOfBirth, Gender, EmployeeCode, EmployeeId) VALUES (@FullName, @DateOfBirth, @Gender, @EmployeeCode, @EmployeeId)";
        //    return sql;
        //    //string execProcedure = "CALL Proc_TenProc(@EmployeeCode, )";
        //    //return execProcedure;
        //}


        /// <summary>
        /// Tạo câu lệnh SQL để thêm mới một nhân viên
        /// </summary>
        /// <returns>sql (string)</returns>
        /// Created By: nkmdang (11/09/2023)
        public static string CreateOneEmployeeWithDepartmentIdSQL()
        {
            //string sql = $"Call Proc_Create_CreateOneEmployeeWithoutDepartmentId(@EmployeeCode, @FullName, @DateOfBirth, @Gender)";
            string sql = $"Call Proc_Create_InsertEmployee(@EmployeeId, @EmployeeCode,  @FullName,  @DepartmentId,  @Gender,  @DateOfBirth,  @Address,  @BankName,  @BankBranch,  @BankAccount,  @Email,  @Mobile,  @LandLinePhone,  @PersonalIdentification,  @PICreatedDate,  @PICreatedPlace,  @PositionName,  @CreatedDate,  @CreatedBy, @ModifiedDate, @ModifiedBy, @AvatarLink)";

            return sql;
        }

        /// <summary>
        /// Tạo câu lệnh SQL để thêm mới một nhân viên sử dụng DepartmentName
        /// </summary>
        /// <returns>sql (string)</returns>
        /// Created By: nkmdang (13/09/2023)
        public static string CreateOneEmployeeWithDepartmentNameSQL()
        {
            string sql = $"Call Proc_Create_CreateOneEmployeeWithDepartmentName(@EmployeeId, @EmployeeCode,  @FullName,  @DepartmentName,  @Gender,  @DateOfBirth,  @Address,  @BankName,  @BankBranch,  @BankAccount,  @Email,  @Mobile,  @LandLinePhone,  @PersonalIdentification,  @PICreatedDate,  @PICreatedPlace,  @PositionName,  @CreatedDate,  @CreatedBy, @ModifiedDate, @ModifiedBy)";

            return sql;
        }

        /// <summary>
        /// Tạo câu lệnh SQL để xóa thông tin một nhân viên theo EmployeeId
        /// </summary>
        /// <param name="employeeId">Id của nhân viên (Guid)</param>
        /// <returns>sql (string)</returns>
        /// Created By: nkmdang (11/09/2023)
        public static string DeleteOneEmployeeByEmployeeIdSQL(Guid employeeId)
        {
            string sql = $"CALL Proc_Delete_DeleteEmployeeById('{employeeId}')";
            return sql;
        }
    }
}
