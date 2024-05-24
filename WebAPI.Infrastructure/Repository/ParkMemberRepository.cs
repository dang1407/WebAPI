using Dapper;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application;
using WebAPI.Domain;

namespace WebAPI.Infrastructure
{
    public class ParkMemberRepository : BaseCompanyRepository<ParkMember>, IParkMemberRepository
    {
        public ParkMemberRepository(IUnitOfWork uow) : base(uow)
        {
        }

        
        
        public async Task<string> GetNewParkMemberCodeAsync()
        {
            // Tạo câu lệnh SQL
            string sql = "SELECT CONCAT('PMB-0', LPAD(MAX(RIGHT(ParkMemberCode, 5)) + 1, 5, '0')) AS NewParkMemberCode FROM parkmember p;";

            // Thực hiện truy vấn
            var result = await Uow.Connection.QuerySingleOrDefaultAsync<string>(sql);
            return result;
        }


        #region Chức năng xuất file excel
        /// <summary>
        /// Hàm xuất file excel ra dạng bytes
        /// </summary>
        /// <param name="parkMembers">Thông tin khách hàng gửi xe cần xuất file excel</param>
        /// <param name="page">Trang khách hàng</param>
        /// <param name="pageSize">Số khách hàng trong trang</param>
        /// <returns>File excel dạng các bytes để truyền về cho clients</returns>
        /// Created by: nkmdang 2/1/2024
        public async Task<byte[]> ExportParkMembersExcelAsync(List<ParkMember> parkMembers, int page, int pageSize)
        {
            // Lấy ra DatePattern từ CSDL
            string sql = "SELECT DatePattern FROM Config c, DateConfiguration d WHERE c.DateConfigurationId = d.DateConfigurationId";
            string datePattern = await Uow.Connection.QueryFirstOrDefaultAsync<string>(sql);


            // Tạo ExcelPackage
            ExcelPackage parkMemberExcelPackage = new ExcelPackage();

            // Tạo WorkSheet
            var workSheet = parkMemberExcelPackage.Workbook.Worksheets.Add("Sheet1");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            workSheet.Cells.Style.Font.Name = "Arial";

            // Định dạng file theo mẫu
            workSheet.Cells["A1:I1"].Merge = true;
            workSheet.Cells["A2:I2"].Merge = true;
            workSheet.Cells["A1:A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A1:A2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells["A1:A2"].Style.Font.Size = 16;
            workSheet.Cells["A1"].Value = "DANH SÁCH KHÁCH HÀNG GỬI XE";
            //workSheet.Row(1).Height = 20;
            workSheet.Cells["A1:I3"].Style.Font.Bold = true;

            // Đặt màu cho dòng tiêu đề
            workSheet.Cells["A3:I3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells["A3:I3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D8D8D8"));
            workSheet.Cells["A3:I3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A3:I3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            // Đặt đường viền cho bảng từ A1 đến hết danh sách
            int numEmployees = parkMembers.Count;
            // Tất cả các ô đều có WrappText = true

            //workSheet.Cells[$"A1:I{numEmployees + 3}"].Style.WrapText = true;
            workSheet.Cells[$"A3:I{numEmployees + 3}"].Style.Font.Name = "Time New Roman";
            workSheet.Cells[$"A4:I{numEmployees + 3}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            workSheet.Cells[$"A4:I{numEmployees + 3}"].Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;
            workSheet.Cells[$"A4:I{numEmployees + 3}"].Style.Font.Size = 11;

            // Các cột ngày căn giữa
            workSheet.Column(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Column(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Kẻ bảng phần dữ liệu
            var cellRange = workSheet.Cells[$"A3:I{numEmployees + 3}"];
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
            workSheet.Cells[3, 2].Value = "Mã khách hàng";
            workSheet.Cells[3, 3].Value = "Tên khách hàng";
            workSheet.Cells[3, 4].Value = "Giới tính";
            workSheet.Cells[3, 5].Value = "Ngày sinh";
            workSheet.Cells[3, 6].Value = "Địa chỉ";
            workSheet.Cells[3, 7].Value = "Biển số xe";
            workSheet.Cells[3, 8].Value = "Ngày đăng ký";
            workSheet.Cells[3, 9].Value = "Số CCCD";
            //workSheet.Cells[3, 10].Value = "Nơi cấp";
            //workSheet.Cells[3, 11].Value = "Địa chỉ";
            //workSheet.Cells[3, 12].Value = "Điện thoại cố định";
            //workSheet.Cells[3, 13].Value = "Điện thoại di động";
            //workSheet.Cells[3, 14].Value = "Email";
            //workSheet.Cells[3, 15].Value = "Tài khoản ngân hàng";
            //workSheet.Cells[3, 16].Value = "Tên ngân hàng";
            //workSheet.Cells[3, 17].Value = "Chi nhánh";


            // Truyền dữ liệu vào bảng
            int recordIndex = 4;
            int firstNumericalOrder = (page - 1) * pageSize + 1;
            foreach (var parkMember in parkMembers)
            {
                workSheet.Cells[recordIndex, 1].Value = firstNumericalOrder;
                workSheet.Cells[recordIndex, 2].Value = parkMember.ParkMemberCode;
                workSheet.Cells[recordIndex, 3].Value = parkMember.FullName;
                if (parkMember.Gender == Gender.Male)
                {
                    workSheet.Cells[recordIndex, 4].Value = "Nữ";
                }
                else if (parkMember.Gender == Gender.FeMale)
                {
                    workSheet.Cells[recordIndex, 4].Value = "Nam";
                }
                else
                {
                    workSheet.Cells[recordIndex, 4].Value = "Khác";
                }
                workSheet.Cells[recordIndex, 5].Value = parkMember.DateOfBirth.HasValue ? parkMember.DateOfBirth.Value.ToString(datePattern) : "";
                workSheet.Cells[recordIndex, 6].Value = parkMember.Address;
                workSheet.Cells[recordIndex, 7].Value = parkMember.LicensePlate;
                workSheet.Cells[recordIndex, 8].Value = parkMember.CreatedDate.Value.ToString(datePattern);
                workSheet.Cells[recordIndex, 9].Value = parkMember.PersonalIdentification;
                recordIndex++;
                firstNumericalOrder++;
            }

            // Hiển thị vừa vặn dữ liệu trên ô excel
            workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
            workSheet.Row(1).Height = 25;
            workSheet.Row(2).Height = 25;
            var columns = workSheet.Cells[workSheet.Dimension.Address].Columns;
            workSheet.Columns[1].Width = 6;
            for (int i = 2; i <= columns; i++)
            {
                workSheet.Columns[i].AutoFit();
            }

            // Trả về dạng các byte
            var excelBytes = parkMemberExcelPackage.GetAsByteArray();

            return excelBytes;
        }

        public async Task<dynamic> IsExistParkMemberAsync(string parkMemberCode)
        {
            string sql = "SELECT * FROM view_read_parkmembers where ParkMemberCode = @parkMemberCode;";
            var param = new DynamicParameters();
            param.Add("@parkMemberCode", parkMemberCode);
            var result = await Uow.Connection.QueryFirstOrDefaultAsync(sql, param, commandType: System.Data.CommandType.StoredProcedure, transaction: Uow.Transaction);
            if(result == null)
            {
                return false;
            } 
            else
            {
                return true;
            }
        }
        #endregion
    }
}
