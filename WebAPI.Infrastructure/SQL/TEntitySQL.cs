using WebAPI.Domain;
using WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace WebAPI.Infrastructure
{
    public class TEntitySQL
    {
    
        /// <summary>
        /// Tạo câu lệnh SQL lấy toàn bộ thông tin nhân viên trong bảng Employee
        /// </summary>
        /// <returns>sql (string)</returns>
        /// Created By: nkmdang (11/09/2023)
        public static string GetAllSQL(string tableName)
        {
            var sql = $"CALL Proc_Read_GetAll{tableName}s()";
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetFilterSQL(string tableName, int page, int pageSize, string? property) 
        {
            var sql = $"Proc_Read_GetAll{tableName}s()";
            return sql;
        }

        /// <summary>
        /// Tạo câu lệnh SQL lấy thông tin nhân viên theo Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>sql (string)</returns>
        /// Created By: nkmdang (11/09/2023)
        public static string GetByIdSQL(string tableName, Guid employeeId)
        {
            string sql = $"SELECT * FROM {tableName} WHERE {tableName}Id = @{tableName}Id";
            return sql;
        }

        /// <summary>
        /// Hàm tạo câu lệnh SQL lấy nhiều theo Id
        /// </summary>
        /// <param name="tableName">Tên bảng muốn lấy ra bản ghi</param>
        /// <returns>Câu lệnh SQL lấy nhiều theo Id </returns>
        /// Created by: nkmdang (21/09/2023)
        public static string GetByListIdSQL(string tableName)
        {
            string sql = $"SELECT * FROM {tableName} WHERE {tableName}Id IN @ids";
            return sql;
        }

        /// <summary>
        /// Tạo câu lệnh SQL để thêm mới một nhân viên sử dụng DepartmentName
        /// </summary>
        /// <returns>sql (string)</returns>
        /// Created By: nkmdang (13/09/2023)
        public static string InsertSQL(IEntity entity)
        {
            string sql;
            if (entity is Employee)
            {
                sql = EmployeeSQL.CreateOneEmployeeWithDepartmentIdSQL();
            }
            else if (entity is Department)
            {
                sql = DepartmentSQL.CreateDepartmentSQL();
            }
            else if(entity is ParkMember)
            {
                sql = ParkMemberSQL.CreateParkMemberSQL();  
            }
            else if(entity is ParkSlot)
            {
                sql = "INSERT INTO parkslot (ParkSlotId, ParkSlotCode, ParkSlotState, Vehicle, ParkingId, VehicleInDate, CreatedDate, CreatedBy) VALUES (@ParkSlotId, @ParkSlotCode, @ParkSlotState, @Vehicle, @ParkingId, @VehicleInDate, @CreatedDate, @CreatedBy);";
            }
            else if(entity is ParkingHistory)
            {
                sql = "INSERT INTO ParkingHistory (ParkingHistoryId, ParkMemberCode, LicensePlate, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, VehicleOutDate, Price, Vehicle) VALUES (@ParkingHistoryId, @ParkMemberCode, @LicensePlate, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy, @VehicleOutDate, @Price, @Vehicle);";
            }
            else
            {
                throw new Exception($"Câu lệnh SQL cho phương thức Insert {typeof(IEntity).Name} chưa được tạo.");
            }
            return sql;
        }

        public static string UpdateSQL(IEntity entity)
        {
            string sql; 
            if (entity is Employee)
            {
                sql = EmployeeSQL.UpdateEmployeeByIdSQL();
            }
            else if (entity is Department)
            {
                sql = DepartmentSQL.UpdateDepartmentSQL();
            }
            else if (entity is Config)
            {
                sql = $"UPDATE Config c SET ConfigId = @ConfigId, DateConfigurationId = @DateConfigurationId, CreatedDate = @CreatedDate, CreatedBy = @CreatedBy, ModifiedDate = @ModifiedDate, ModifiedBy = @ModifiedBy;";
            }
            else if (entity is ParkMember)
            {
                sql = ParkMemberSQL.UpdateParkMemberSQL();
            }
            else if(entity is ParkSlot)
            {
                sql = "Update ParkSlot SET ParkSlotState = @ParkSlotState WHERE ParkSlotId = @ParkSlotId;";
            }
            else if(entity is ParkingHistory)
            {
                sql = "UPDATE ParkingHistory SET ParkMemberCode = @ParkMemberCode, LicensePlate = @LicensePlate, CreatedDate = @CreatedDate, CreatedBy = @CreatedBy, ModifiedDate = @ModifiedDate, ModifiedBy = @ModifiedBy, VehicleOutDate = @VehicleOutDate, Price = @Price, Vehicle = @Vehicle;";
            }
            else 
            {
                throw new Exception($"Câu lệnh SQL cho phương thức Update {typeof(IEntity).Name} chưa được tạo.");
            }

            return sql;
        }

        /// <summary>
        /// Tạo câu lệnh SQL để xóa thông tin một nhân viên theo EmployeeId
        /// </summary>
        /// <param name="employeeId">Id của nhân viên (Guid)</param>
        /// <returns>sql (string)</returns>
        /// Created By: nkmdang (11/09/2023)
        public static string DeleteByIdSQL(string tableName, Guid id)
        {
            //string sql = $"CALL Proc_Delete_Delete{tableName}ById('{employeeId}')";
            string sql = $"DELETE FROM {tableName} WHERE {tableName}Id = @Id";
            return sql;
        }

        /// <summary>
        /// Tạo câu lệnh SQL để xóa nhiều bản ghi theo List Id
        /// </summary>
        /// <param name="tableName">Tên bảng muốn xóa các bản ghi</param>
        /// <returns>Câu lệnh SQL</returns>
        /// Created by: nkmdang (21/09/2023)
        public static string DeleteByListIdSQL(string tableName)
        {
            string sql = $"DELETE FROM {tableName} WHERE {tableName}Id IN @ids";
            return sql;
        }
    }
}
