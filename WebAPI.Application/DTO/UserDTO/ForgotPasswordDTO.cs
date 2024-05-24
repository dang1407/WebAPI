using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class ForgotPasswordDTO : BaseDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RewritePassword { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;    
    }
}
