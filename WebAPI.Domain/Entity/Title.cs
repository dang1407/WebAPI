using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public class Title : BaseEntity, IEntity
    {
        public Guid TitleId { get; set; }
        public string TitleName { get; set; } = string.Empty; 
        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;
        public Guid GetId()
        {
            return TitleId;
        }

        public void SetId(Guid id)
        {
            TitleId = id;
        }
    }
}
