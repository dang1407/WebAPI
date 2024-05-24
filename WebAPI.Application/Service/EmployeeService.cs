using WebAPI.Application;
using WebAPI;
using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Resources;
using WebAPI.Domain.Resource;

namespace WebAPI.Application
{
    public class EmployeeService : BaseCompanyService<Employee, EmployeeDTO, EmployeeCreateDTO, EmployeeUpdateDTO>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeValidate _employeeValidate;
        private readonly ITitleRepository _titleRepository;
        public EmployeeService(IEmployeeRepository employeeRepository, IEmployeeValidate employeeValidate, 
          ITitleRepository titleRepository , IMapper mapper) : base(employeeRepository, mapper)
        {
            _employeeRepository = employeeRepository;
            _employeeValidate = employeeValidate;   
            _titleRepository = titleRepository; 
        }


        


        

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới (string)</returns>
        /// Created by: nkmdang (27/09/2023)
        public async Task<string> GetNewEmployeeCodeAsync(Guid companyId)
        {
            var result = await _employeeRepository.GetNewEmployeeCodeAsync(companyId);   
            return result;  
        }


        /// <summary>
        /// Hàm Validate nghiệp vụ cho thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entity">Thông tin Entity thêm mới</param>
        /// <returns>Ném ra lỗi nếu đầu vào không thỏa mãn nghiệp vụ</returns>
        /// Created by: nkmdang (19/09/2023)
        public override async Task ValidateCreateBusinessAsync(Employee employee, Guid companyId)
        {

            // Mã nhân viên mới nhập chưa tồn tại
            var existEmployeeCode = await BaseCompanyRepository.GetFilterAsync(1, 1, employee.EmployeeCode, companyId);
            if (existEmployeeCode.Count > 0) 
            {
                throw new ConflictException("Mã nhân viên đã tồn tại");
            }

            // Chức danh và đơn vị phải hợp lệ
            var numberTitle = await _titleRepository.CheckExistTitleByTitleIdAndDepartmentIdAsync(employee.TitleId, employee.DepartmentId, companyId); 
            if(numberTitle != 1)
            {
                throw new NotFoundException("Lỗi TitleId và DepartmentId");
            }
        }

        /// <summary>
        /// Hàm Validate nghiệp vụ cho sửa đổi 1 bản ghi
        /// </summary>
        /// <param name="entity">Thông tin Entity sửa đổi</param>
        /// <returns>Ném ra lỗi nếu đầu vào không thỏa mãn nghiệp vụ</returns>
        /// Created by: nkmdang (19/09/2023)
        public async override Task ValidateUpdateBusinessAsync(Employee employee, Guid companyId)
        {
            // Mã nhân viên sửa đổi có thể trùng mã nhân viên cũ, không trùng mã nhân viên người khác
            var existEmployee = await BaseCompanyRepository.GetFilterAsync(1, 2, employee.EmployeeCode, companyId);
            if(existEmployee.Count > 1)
            {
                throw new ConflictException("Mã nhân viên trùng đã tồn tại");
            } 
        }

        

        

        #region Chức năng xuất file excel
        /// <summary>
        /// Hàm xuất thông tin nhân viên ra excel
        /// </summary>
        /// <param name="employeeDTOs">Thông tin nhân viên</param>
        /// <param name="page">Số thứ tự trang</param>
        /// <param name="pageSize">Số bản ghi trong trang</param>
        /// <returns></returns>
        /// Created by: nkmdang 08/10/2023
        public async Task<byte[]> ExportEmployeeExcelAsync(List<EmployeeDTO> employeeDTOs, int page, int pageSize)
        {
            // Chuyển đổi DTO sang Entity các nhân viên tìm được
            var employees = employeeDTOs.Select(employeeDTO => MapDTOToEntity(employeeDTO)).ToList();   

            // Gọi EmployeeRepository để lấy dữ liệu dạng các byte
            var excelBytes = await _employeeRepository.ExportEmployeeExcelAsync(employees, page, pageSize); 
            
            // Trả về bytes cho Controller
            return excelBytes;
        }





        #endregion


        /// <summary>
        /// Hàm lấy ra thông tin nhiều nhân viên theo danh sách Id
        /// </summary>
        /// <param name="ids">Danh sách Id của nhân viên</param>
        /// <returns>Thông tin các nhân viên tìm thấy</returns>
        /// Created By: nkdang 31/10/2023
        public async Task<List<EmployeeDTO>> GetByListIdAsync(List<Guid> ids, Guid companyId)
        {
            var employees = await _employeeRepository.GetByListIdAsync(ids, companyId);
            var employeeDTOS = employees.Select(employee => MapEntityToDTO(employee)).ToList();
            return employeeDTOS;
        }
    }
}

