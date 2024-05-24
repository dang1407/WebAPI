using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using WebAPI.Application;
using WebAPI.Domain;

namespace WebAPI.Controllers
{
    
    public class BaseCompanyController<TDTO, TCreateDTO, TUpdateDTO> : BaseCompanyReadOnlyController<TDTO>
    {
        protected readonly IBaseCompanyService<TDTO, TCreateDTO, TUpdateDTO> BaseCompanyService;

        public BaseCompanyController(IBaseCompanyService<TDTO, TCreateDTO, TUpdateDTO> baseCompanyService) : base(baseCompanyService)
        {
            BaseCompanyService = baseCompanyService;
        }

        /// <summary>
        /// Hàm thêm mới một Entity
        /// </summary>
        /// <param name="createDTO">DTO tạo mới entity</param>
        /// <returns>Thông tin Entity tạo mới nếu thành công</returns>
        /// Created by: nkmdang (21/09/2023)
        [HttpPost]
        [Route("")]
        [Authorize]
        public virtual async Task<IActionResult> InsertAsync([FromBody] TCreateDTO createDTO)
        {
            Guid companyId = Guid.Parse( HttpContext.User.FindFirstValue("CompanyId"));
            var errors = GetModelStateError(ModelState);
            if(errors != null)
            {
                return BadRequest(errors);
            }
            var result = await BaseCompanyService.InsertAsync(createDTO, companyId);  
            return StatusCode(201, result);  
        }

        [HttpPost]
        [Route("insertMany")]
        [Authorize]
        public async Task<IActionResult> InsertManyAsync([FromBody] List<TCreateDTO> createDTOs)
        {
            Guid companyId = Guid.Parse(HttpContext.User.FindFirstValue("CompanyId"));
            var results = await BaseCompanyService.InsertManyAsync(createDTOs, companyId);
            return Ok(results);
        }

        /// <summary>
        /// Hàm sửa thông tin một TDTO
        /// </summary>
        /// <param name="updateDTO">Instance của TDTO</param>
        /// <returns>Thông tin của TDTO sau khi đã thay đổi</returns>
        /// Created by: nkmdang (20/09/2023)
        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public virtual async Task<IActionResult> UpdateAsync( Guid id, [FromBody] TUpdateDTO updateDTO)
        {
            Guid companyId = Guid.Parse(HttpContext.User.FindFirstValue("CompanyId"));
            var errors = GetModelStateError(ModelState);
            if (errors != null)
            {
                return BadRequest(errors);
            }
            var result = await BaseCompanyService.UpdateAsync(id, updateDTO, companyId);
            return Ok(result);
        }


        /// <summary>
        /// Hàm xóa thông tin một TDTO
        /// </summary>
        /// <param name="id">Định danh TDTO</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: nkmdang (20/09/2023)
        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<int> DeleteAsync(Guid id)
        {
            Guid companyId = Guid.Parse(HttpContext.User.FindFirstValue("CompanyId"));
            var result = await BaseCompanyService.DeleteAsync(id, companyId);
            return result;
        }


        /// <summary>
        /// Hàm xóa thông tin nhiều TDTO
        /// </summary>
        /// <param name="ids">Danh sách các dịnh danh TDTO</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: nkmdang (20/09/2023)
        [HttpDelete]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> DeleteManyAsync(List<Guid> ids)
        {
            Guid companyId = Guid.Parse(HttpContext.User.FindFirstValue("CompanyId"));
            var result = await BaseCompanyService.DeleteManyAsync(ids, companyId);
            var response = new
            {
                Success = $"Xóa thành công {result} bản ghi!",
                Error = $"Xóa thất bại {ids.Count - result} bản ghi!"
            };
            return StatusCode(200, response);
        }

        private Object? GetModelStateError(ModelStateDictionary ModelState)
        {
            if (!ModelState.IsValid)
            {
                // Danh sách thông tin lỗi cho từng trường
                List<ErrorDetail> errorDetails = new List<ErrorDetail>();

                // Lặp qua các lỗi trong ModelState và lấy tên trường và thông báo lỗi
                foreach (var key in ModelState.Keys)
                {
                    if (ModelState[key].ValidationState == ModelValidationState.Invalid)
                    {

                        var error = new ErrorDetail
                        {
                            FieldName = key,
                            ErrorMessage = ModelState[key].Errors[0].ErrorMessage
                        };
                        errorDetails.Add(error);
                    }
                }
                // Trả về danh sách các trường thông tin bị lỗi cùng với thông báo lỗi
                return errorDetails;
            }
            else return null;
        }
    }
}
