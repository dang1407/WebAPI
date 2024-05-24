using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateNotInFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                if (value is DateTimeOffset date)
                {
                    // Lấy ngày tháng hiện tại
                    var currentDate = DateTime.Now.Date;

                    // So sánh ngày tháng trong giá trị được truyền vào với ngày tháng hiện tại
                    if (date.Date > currentDate)
                    {
                        return new ValidationResult(ErrorMessage); // Trả về false nếu ngày tháng lớn hơn ngày tháng hiện tại
                    }
                }
            }
            

            return ValidationResult.Success; // Trả về true nếu không có lỗi kiểm tra
        }
    }
}
