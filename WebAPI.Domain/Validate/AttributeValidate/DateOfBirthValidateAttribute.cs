using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public class DateOfBirthValidateAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;
        private readonly int _maximumAge;   
        public DateOfBirthValidateAttribute(int minimumAge, int maximumAge)
        {
            _minimumAge = minimumAge;
            _maximumAge = maximumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Chấp nhận giá trị null là hợp lệ
            if(value == null) 
            {
                return ValidationResult.Success;
            }

            // Nếu ngày tháng không null thì mới kiểm tra
            if (value is DateTimeOffset dateOfBirth)
            {
                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Year;
                Console.WriteLine("age = " + age);
                Console.WriteLine("Todat = " + today);
                if (dateOfBirth > today.AddYears(-age))
                    age--;

                if (age >= _minimumAge && age <= _maximumAge)
                    return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
