using Dapper;
using WebAPI.Application;
using WebAPI.Domain;
using MySqlConnector;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static Dapper.SqlMapper;
using System.Data;
using System.Drawing.Printing;

namespace WebAPI.Infrastructure
{
    public class EmployeeRepository : BaseCompanyRepository<Employee> ,IEmployeeRepository
    {
        public EmployeeRepository(IUnitOfWork uow) : base(uow)
        {
        }

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới (string)</returns>
        /// Created by: nkmdang (27/09/2023)
        public async Task<string> GetNewEmployeeCodeAsync(Guid companyId)
        {
            // Tạo câu lệnh SQL
            string sql = $"SELECT CONCAT('NV-', LPAD(CAST(SUBSTRING(MAX(EmployeeCode), 6) AS UNSIGNED) + 1, 6, '0')) AS new_employee_code FROM employee e, title t, department d where e.TitleId = t.TitleId AND t.DepartmentId = d.DepartmentId and d.CompanyId = @companyId;";
            var param = new DynamicParameters();
            param.Add("companyId", companyId.ToString());
            // Thực hiện truy vấn
            var result = await Uow.Connection.QuerySingleAsync<string>(sql,param, transaction: Uow.Transaction);
            return result;
        }


        /// <summary>
        /// Hàm kiểm tra nhân viên tồn tại bằng Mã nhân viên (EmployeeCode)
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên (string)</param>
        /// <returns>Thông tin nhân viên nếu tìm thấy, null nếu không tìm thấy</returns>
        /// Created by: nkmdang (18/09/2023)
        public async Task<dynamic> IsExistEmployeeAsync(string employeeCode, Guid companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_Page", 0, DbType.Int32);
            parameters.Add("p_PageSize", 1, DbType.Int32);
            parameters.Add("p_SearchProperty", employeeCode, DbType.String);
            parameters.Add("p_CompanyId", companyId, DbType.String);
            // Thực hiện truy vấn
            var result = await Uow.Connection.QueryAsync<Employee>($"Proc_Read_EmployeesFilter", parameters, commandType: CommandType.StoredProcedure, transaction: Uow.Transaction);
            return result.ToList();
        }


        #region Chức năng tìm kiếm nhân viên
        /// <summary>
        /// Hàm tìm kiếm nhân viên theo số điện thoại
        /// </summary>
        /// <param name="page">Số thứ tự trang</param>
        /// <param name="pageSize">Số nhân viên mỗi trang</param>
        /// <param name="mobile">Số điện thoại nhân viên</param>
        /// <returns>Thông tin các nhân viên tìm được</returns>
        /// /// Created by: nkmdang (2/10/2023)
        public async Task<Employee> GetEmployeeByMobileAsync(int page, int pageSize, string mobile)
        {
            // Tạo câu lệnh SQL
            string sql = $"SELECT * FROM view_read_getemployeespagination ve WHERE ve.Mobile = @Mobile ORDER BY SUBSTRING_INDEX(ve.FullName, ' ', -1), ve.FullName ASC, ve.EmployeeCode ASC LIMIT @PageSize OFFSET @Page";

            // Tạo param 
            var param = new DynamicParameters();
            param.Add("Mobile", mobile);
            param.Add("PageSize", pageSize);
            // Offset trong database bắt đầu từ 0, còn FE là bắt đầu từ 1
            param.Add("Page", page - 1);

            // Thực hiện truy vấn
            var result = await Uow.Connection.QuerySingleOrDefaultAsync<Employee>(sql, param);
            return result;
        }

        /// <summary>
        /// Hàm tìm kiếm nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>Thông tin nhân viên tìm được</returns>
        /// Created by: nkmdang (27/09/2023)
        public async Task<Employee> GetEmployeeByEmployeeCodeAsync(string employeeCode)
        {
            // Tạo câu lệnh sql
            string sql = EmployeeSQL.GetEmployeeByEmployeeCodeSQL();

            // Tạo param
            var param = new DynamicParameters(employeeCode);
            param.Add("EmployeeCode", employeeCode);
            // Thực thi truy vấn
            var result = await Uow.Connection.QuerySingleOrDefaultAsync<Employee>(sql, param);
            return result;
        }

        /// <summary>
        /// Hàm tìm kiếm nhân viên theo họ tên đầy đủ nhân viên
        /// </summary>
        /// <param name="employeeFullName">Họ tên đầy đủ nhân viên</param>
        /// <returns>Thông tin nhân viên tìm được</returns>
        /// Created by: nkmdang (27/09/2023)
        public async Task<List<Employee>> GetEmployeeByFullNameAsync(int page, int pageSize, string employeeFullName)
        {
            // Tạo câu lệnh SQL
            string sql = $"SELECT * FROM view_read_getemployeespagination ve WHERE FullName LIKE '%{employeeFullName}%' ORDER BY SUBSTRING_INDEX(ve.FullName, ' ', -1), ve.EmployeeCode ASC LIMIT @PageSize OFFSET @PAGE";
            var param = new DynamicParameters();
            param.Add("FullName", employeeFullName);
            param.Add("PageSize", pageSize);
            param.Add("Page", page - 1);
            param.Add("Name", employeeFullName);
            var result = await Uow.Connection.QueryAsync<Employee>(sql, param);
            return result.ToList();
        }
        #endregion

        #region Chức năng xuất file excel
        /// <summary>
        /// Hàm tạo ra các bytes dữ liệu của file excel để gửi về trình duyệt
        /// </summary>
        /// <param name="employees">Thông tin nhân viên cần chuyển sang Excel</param>
        /// <param name="page">Số tự trang nhân viên</param>
        /// <param name="pageSize">Số nhân viên cần chuyển</param>
        /// <returns>File Excel dạng các bytes</returns>
        /// Created By: nkmdang 09/10/2023
        public async Task<byte[]> ExportEmployeeExcelAsync(List<Employee> employees, int page, int pageSize)
        {
            // Lấy ra DatePattern từ CSDL
            //string sql = "SELECT DatePattern FROM Config c, DateConfiguration d WHERE c.DateConfigurationId = d.DateConfigurationId";
            //string datePattern = await Uow.Connection.QueryFirstOrDefaultAsync<string>(sql);


            // Tạo ExcelPackage
            ExcelPackage employeeExcelPackage = new ExcelPackage();
            
            // Tạo WorkSheet
            var workSheet = employeeExcelPackage.Workbook.Worksheets.Add("Sheet1");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            workSheet.Cells.Style.Font.Name = "Arial";

            // Định dạng file theo mẫu
            workSheet.Cells["A1:Q1"].Merge = true;
            workSheet.Cells["A2:Q2"].Merge = true;
            workSheet.Cells["A1:A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A1:A2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;  
            workSheet.Cells["A1:A2"].Style.Font.Size = 16;
            workSheet.Cells["A1"].Value = "DANH SÁCH NHÂN VIÊN";
            //workSheet.Row(1).Height = 20;
            workSheet.Cells["A1:Q3"].Style.Font.Bold = true;

            // Đặt màu cho dòng tiêu đề
            workSheet.Cells["A3:Q3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells["A3:Q3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D8D8D8"));
            workSheet.Cells["A3:Q3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A3:Q3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            // Đặt đường viền cho bảng từ A1 đến hết danh sách
            int numEmployees = employees.Count;
            // Tất cả các ô đều có WrappText = true
            
            //workSheet.Cells[$"A1:Q{numEmployees + 3}"].Style.WrapText = true;
            workSheet.Cells[$"A3:Q{numEmployees + 3}"].Style.Font.Name = "Time New Roman";
            workSheet.Cells[$"A4:Q{numEmployees + 3}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            workSheet.Cells[$"A4:Q{numEmployees + 3}"].Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;
            workSheet.Cells[$"A4:Q{numEmployees + 3}"].Style.Font.Size = 11;

            // Các cột ngày căn giữa
            workSheet.Column(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Column(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Kẻ bảng phần dữ liệu
            var cellRange = workSheet.Cells[$"A3:Q{numEmployees + 3}"];
            var borderStyle = ExcelBorderStyle.Thin;
            var borderColor = System.Drawing.Color.Black;

            cellRange.Style.Border.Top.Style = borderStyle;
            cellRange.Style.Border.Bottom.Style = borderStyle;
            cellRange.Style.Border.Left.Style = borderStyle;
            cellRange.Style.Border.Right.Style = borderStyle;

            cellRange.Style.Border.Top.Color.SetColor(borderColor);
            cellRange.Style.Border.Bottom.Color.SetColor(borderColor);
            cellRange.Style.Border.Left.Color.SetColor(borderColor);
            cellRange.Style.Border.Right.Color.SetColor(borderColor);

            workSheet.Cells[3, 1].Value = "STT";
            workSheet.Cells[3, 2].Value = "Mã nhân viên";
            workSheet.Cells[3, 3].Value = "Tên nhân viên";
            workSheet.Cells[3, 4].Value = "Giới tính";
            workSheet.Cells[3, 5].Value = "Ngày sinh";
            workSheet.Cells[3, 6].Value = "Chức danh";
            workSheet.Cells[3, 7].Value = "Tên đơn vị";
            workSheet.Cells[3, 8].Value = "Số CMND";
            workSheet.Cells[3, 9].Value = "Ngày cấp";
            workSheet.Cells[3, 10].Value = "Nơi cấp";
            workSheet.Cells[3, 11].Value = "Địa chỉ";
            workSheet.Cells[3, 12].Value = "Điện thoại cố định";
            workSheet.Cells[3, 13].Value = "Điện thoại di động";
            workSheet.Cells[3, 14].Value = "Email";
            workSheet.Cells[3, 15].Value = "Tài khoản ngân hàng";
            workSheet.Cells[3, 16].Value = "Tên ngân hàng";
            workSheet.Cells[3, 17].Value = "Chi nhánh";

            
            // Truyền dữ liệu vào bảng
            int recordIndex = 4;
            int firstNumericalOrder = (page - 1) * pageSize + 1;
            foreach(var employee in employees)
            {
                workSheet.Cells[recordIndex, 1].Value = firstNumericalOrder;
                workSheet.Cells[recordIndex, 2].Value = employee.EmployeeCode;
                workSheet.Cells[recordIndex, 3].Value = employee.FullName;
                if(employee.Gender == Gender.Male)
                {
                workSheet.Cells[recordIndex, 4].Value = "Nữ";
                } 
                else if(employee.Gender == Gender.FeMale)
                {
                    workSheet.Cells[recordIndex, 4].Value = "Nam";
                } 
                else
                {
                    workSheet.Cells[recordIndex, 4].Value = "Khác";
                }
                workSheet.Cells[recordIndex, 5].Value = employee.DateOfBirth.HasValue ? employee.DateOfBirth.Value.ToString("dd/MM/yyyy") : "";

                workSheet.Cells[recordIndex, 8].Value = employee.PersonalIdentification;
                workSheet.Cells[recordIndex, 9].Value = employee.PICreatedDate.HasValue ? employee.DateOfBirth.Value.ToString("dd/MM/yyyy") : "";
                workSheet.Cells[recordIndex, 10].Value = employee.PICreatedPlace;
                workSheet.Cells[recordIndex, 11].Value = employee.Address;
                workSheet.Cells[recordIndex, 13].Value = employee.Mobile;
                workSheet.Cells[recordIndex, 14].Value = employee.Email;
                workSheet.Cells[recordIndex, 15].Value = employee.BankAccount;
                workSheet.Cells[recordIndex, 16].Value = employee.BankName;
                workSheet.Cells[recordIndex, 17].Value = employee.BankBranch;
                recordIndex++;
                firstNumericalOrder++;
            }

            // Hiển thị vừa vặn dữ liệu trên ô excel
            workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
            workSheet.Row(1).Height = 25;
            workSheet.Row(2).Height = 25;
            //for(int i = 3; i <=numEmployees + 3; i++)
            //{
            //workSheet.Row(i).Height = 15;
            //}

            //for (int col = 1; col <= workSheet.Dimension.End.Column; col++)
            //{
            //    // Lấy nội dung của tiêu đề trong cột
            //    string headerText = workSheet.Cells[3, col].Text;
            //    string valueText = workSheet.Cells[3, col].Text;
            //    // Tính toán chiều rộng mới cho cột
            //    //double minWitdh = headerText.Length * 1.25 > valueText*1.25 ? ;

            //    // Thiết lập chiều rộng của cột
            //    //workSheet.Column(col).AutoFit(minWitdh, 100);
            //}

            //var colums = workSheet.Columns;
            var columns = workSheet.Cells[workSheet.Dimension.Address].Columns;
            workSheet.Columns[1].Width = 6;
            for (int i = 2; i <= columns; i++)
            {
                workSheet.Columns[i].AutoFit();
            }

            // Trả về dạng các byte
            var excelBytes = employeeExcelPackage.GetAsByteArray();

            return excelBytes;
        }

    

        public Task<List<Employee>> GetByListIdAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }


        #endregion

        ///// <summary>
        ///// Hàm lấy ra thông tin nhiều nhân viên theo danh sách Id
        ///// </summary>
        ///// <param name="ids">Danh sách Id của nhân viên</param>
        ///// <returns>Thông tin các nhân viên tìm thấy</returns>
        ///// Created By: nkdang 31/10/2023
        //public async Task<List<Employee>> GetByListIdsAsync(List<Guid> ids)
        //{
        //    string sql = $"Select ve.EmployeeId, ve.EmployeeCode, ve.FullName, ve.DepartmentName, ve.PositionName, ve.PaidLeaveDaysUsed, ve.CompensatoryLeaveDaysUsed, ve.AnotherLeavesDayUsed FROM view_read_getemployeespagination ve WHERE ve.EmployeeId in @ids";
        //    var result = await Uow.Connection.QueryAsync<Employee>(sql, ids);
        //    return result.ToList();
        //}
    }
}
