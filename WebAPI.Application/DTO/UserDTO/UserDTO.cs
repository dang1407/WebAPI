using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class UserDTO : BaseDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; 
        
        public string Role { get; set; } = string.Empty;    
        public string RefreshToken {  get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public Guid CompanyId { get; set; } 
    }
}
