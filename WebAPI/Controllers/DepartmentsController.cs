using Microsoft.AspNetCore.Mvc;
using WebAPI.Application;

namespace WebAPI.Controllers
{

    public class DepartmentsController : BaseCompanyReadOnlyController<DepartmentDTO>
    {
        private readonly IDepartmentService _departmentsService;    
        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {
            _departmentsService = departmentService;
        }



        /// <summary>
        /// Hàm lấy ra Department theo tên
        /// </summary>
        /// <param name="departmentName">Tên của Department (string)</param>
        /// <returns>Thông tin department</returns>
        /// Created by: nkmdang (21/09/2023)
        [HttpGet]
        [Route("DepartmentName")]
        public async Task<DepartmentDTO> GetDepartmentByNameAsync(string departmentName)
        {
            var result = await _departmentsService.GetDepartmentByNameAsync(departmentName);
            return result;
        }


        ///// <summary>
        ///// Hàm lấy ra tất cả thông tin về đơn vị
        ///// </summary>
        ///// <returns>Thông tin đơn vị</returns>
        ///// Created by: nkmdang (28/09/2023)
        //[HttpGet]
        //[Route("")]
        //public async Task<List<DepartmentDTO>> GetDepartmentDatasAsync(int page, int pageSize, string? departmentProperty)
        //{
        //    DepartmentDTO dto = new DepartmentDTO();
        //    // Page ở client lớn hơn page ở backend 1 đơn vị
        //    var result = await _departmentsService.GetFilterAsync(dto, page - 1, pageSize, departmentProperty);   
        //    return result;
        //}
    }
}
