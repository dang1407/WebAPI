using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public class Company : BaseEntity, IEntity
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public Guid GetId()
        {
            return CompanyId;
        }

        public void SetId(Guid Id)
        {
            CompanyId = Id; 
        }
    }
}
