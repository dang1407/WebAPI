using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class AccountCreateDTO
    {
        public Guid AccountId { get; set; } 
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RewritePassword { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;  
        public Guid CompanyId { get; set; } 

    }
}
