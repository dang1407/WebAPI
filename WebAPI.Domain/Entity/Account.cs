using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
     public class Account : BaseEntity, IEntity
    {
        public Guid AccountId { get; set; }    
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty; 
        public Guid CompanyId { get; set; } 
        public Guid GetId()
        {
            return AccountId;
        }

        public void SetId(Guid id)
        {
            AccountId = id;    
        }
    }
}
