using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Infrastructure
{
    public class ParkMemberSQL
    {

        public static string GetParkMembersPaginationsSQL(int page, int pageSize)
        {
            string sql = $"CALL Proc_Read_GetParkMembersFilter({page}, {pageSize});";
            return sql;
        }

        public static string CreateParkMemberSQL()
        {
            string sql = "INSERT INTO parkmember (ParkMemberId,  FullName,  ParkMemberCode,  PersonalIdentification,  CreatedDate,  CreatedBy,  ModifiedDate,  ModifiedBy,  DateOfBirth,  Address,  LicensePlate,  AvatarLink,  Mobile, Gender) VALUES ( @ParkMemberId,  @FullName,  @ParkMemberCode,  @PersonalIdentification,  @CreatedDate,  @CreatedBy,  @ModifiedDate,  @ModifiedBy,  @DateOfBirth,  @Address,  @LicensePlate,  @AvatarLink,  @Mobile, @Gender );";
            return sql;
        }

        public static string UpdateParkMemberSQL() 
        {
            string sql = "Update ParkMember SET FullName = @FullName,  ParkMemberCode = @ParkMemberCode,  PersonalIdentification = @PersonalIdentification,  CreatedDate = @CreatedDate,  CreatedBy = @CreatedBy,  ModifiedDate = @ModifiedDate,  ModifiedBy = @ModifiedBy,  DateOfBirth = @DateOfBirth,  Address = @Address,  LicensePlate = @LicensePlate,  AvatarLink = @AvatarLink,  Mobile = @Mobile, Gender = @Gender WHERE ParkMemberId = @ParkMemberId;";
            return sql; 
        }
    }
}
