using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public static class EmployeeResources
    {
        public static readonly string DateOfBirthNotInFuture = "Ngày sinh không thể lớn hơn ngày hiện tại.";
        public static readonly string DateOfBirthEmployeeNotValidate = "Ngày sinh phải thỏa mãn tuổi nhân viên lớn hơn 18 và không vượt quá 65.";
    }
}
