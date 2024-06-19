using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public class ParkMember : BaseEntity, IEntity
    {
        public Guid ParkMemberId { get; set; }  
        public string ParkMemberCode { get; set; } = string.Empty;  
        public string FullName { get; set; } = string.Empty;

        public string Email {  get; set; } = string.Empty;
        public string PersonalIdentification { get; set; } = string.Empty;

        public long? TotalPrice { get; set; }    

        public DateTimeOffset? DateOfBirth { get; set; }  

        public string? Address { get; set; } = string.Empty; 
        public string LicensePlate { get; set; } = string.Empty;

        public string? AvatarLink { get; set; } = string.Empty;
        public string? Mobile { get; set; } = string.Empty;
        public Gender? Gender { get; set; } 
        public Guid AccountId { get; set; } 
        public Guid GetId()
        {
            return ParkMemberId;    
        }

        public void SetId(Guid id)
        {
            ParkMemberId = id;  
        }
    }
}
