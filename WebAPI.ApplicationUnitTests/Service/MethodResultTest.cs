using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application;
using WebAPI.Domain;

namespace WebAPI.ApplicationUnitTests
{
    public class MethodResultTest
    {
        private IMapper Mapper;
        public MethodResultTest(IMapper mapper) 
        {
            Mapper = mapper;   
        }

        //public void Main(string[] args)
        //{
        //    var employeeUpdateDTO = new EmployeeUpdateDTO()
        //    {
        //        EmployeeCode = "NV-00673",
        //        FullName = "Nguyễn Minh Dũng",
        //        Gender = Gender.Male,
        //        DepartmentId = new Guid("26021185-77af-263c-842a-acc8fc2f00af"),
        //        DepartmentName = "Marketing ",
        //        LandLinePhone = "(491) 479-4270",
        //        Mobile = "+1 366-729-3627",
        //        PositionName = "Nhân viên phân tích nghiệp vụ",
        //        Address = "3806 W Highland Highway",
        //        BankAccount = "51941",
        //        BankBranch = "Hà Nội",
        //        BankName = "ACB ",
        //        PersonalIdentification = "025237871085",
        //        PICreatedDate = Convert.ToDateTime("2021-02-19T00:00:00+07:00"),
        //        PICreatedPlace = "Hà Nội",
        //        Email = "kvijx7116@gmail.com",

        //        CreatedBy = "Dianne Stack",


        //        ModifiedBy = "Porsche Rudolph"
        //    };
        //    var employee = new Employee()
        //    {
        //        EmployeeId = Guid.NewGuid(),
        //    };

        //    var result = Mapper.Map(employeeUpdateDTO, employee);
        //    Console.WriteLine(result);  
        //}
    }
}
