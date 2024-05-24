//using AutoMapper;
//using WebAPI.Domain;
//using NSubstitute;
//using System.ComponentModel;

//namespace WebAPI.Application.UnitTests
//{
//    [TestFixture]
//    public class EmployeeServiceTests
//    {
//        public IEmployeeRepository EmployeeRepository { get; set; } 
//        public IEmployeeValidate EmployeeValidate { get; set; } 
//        public IMapper EmployeeMapper { get; set; } 
//        public EmployeeService EmployeeService { get; set; }

//        #region Setup
//        /// <summary>
//        /// Hàm setup các đối tượng giả phục vụ việc test
//        /// </summary>
//        /// Created by: nkmdang (26/09/2023)
//        [SetUp]
//        public void SetUp() 
//        {
//            EmployeeRepository = Substitute.For<IEmployeeRepository>(); 
//            EmployeeValidate = Substitute.For<IEmployeeValidate>(); 
//            EmployeeMapper = Substitute.For<IMapper>(); 
//            EmployeeService = Substitute.For<EmployeeService>(EmployeeRepository, EmployeeValidate, EmployeeMapper);   
//        }
//        #endregion

//        #region Test các hàm Get

//        ///// <summary>
//        ///// Hàm test hàm lấy ra dữ liệu tất cả nhân viên
//        ///// </summary>
//        ///// <returns></returns>
//        ///// Created by: nkmdang (26/09/2023)
//        //[Test]
//        //public async Task GetFilterAsync_GetSuccess_Success()
//        //{
//        //    // Arrange
//        //    EmployeeRepository.GetFilterAsync().Returns(new List<Employee>()); 
//        //    // Act 
//        //    var result = await EmployeeService.GetFilterAsync();
//        //    // Assert
//        //    await EmployeeRepository.Received(1).GetFilterAsync(); 
//        //}


//        /// <summary>
//        /// Hàm test hàm tìm kiếm một nhân viên
//        /// </summary>
//        /// <returns></returns>
//        /// Created by: nkmdang (26/09/2023)
//        [Test]
//        public async Task FindByIdAsync_FindSuccess_Success()
//        {
//            // Arrange
//            var id = Guid.NewGuid();
//            // Act 
//            var result = await EmployeeService.FindByIdAsync(id);
//            // Assert
//            await EmployeeRepository.Received(1).FindByIdAsync(id);
//        }

//        /// <summary>
//        /// Hàm test hàm lấy ra dữ liệu một nhân viên
//        /// </summary>
//        /// <returns></returns>
//        /// Created by: nkmdang (26/09/2023)
//        [Test]
//        public async Task GetByIdAsync_GetSuccess_Success()
//        {
//            // Arrange
//            var id = Guid.NewGuid();
//            // Act 
//            var result = await EmployeeService.GetByIdAsync(id);
//            // Assert
//            await EmployeeRepository.Received(1).GetByIdAsync(id);   
//        }

//        [Test]
//        public async Task GetEmployeeByFullNameAsync_NotFoundEmployee_ThrowException()
//        {
//            // Arrange
//            var fullName = "FullNameTest";
//            int page = 1, pageSize = 10;
//            var employee = new Employee()
//            {
//                FullName = fullName,
//            };
//            var result = new List<Employee>();
//            result.Add(employee);

//            // Tạo kết quả giả cho hàm lấy nhân viên từ CSDL
//            EmployeeRepository.GetEmployeeByFullNameAsync(page, pageSize, fullName).Returns(result);
//            // Chuyển đổi kết quả giả từ Employee sang EmployeeDTO thành kết quả mong đợi
//            var expectResult = result.Select(employee => EmployeeService.MapEntityToDTO(employee)).ToList(); 


//            // Act 
//            var actualResult = await EmployeeService.GetEmployeeByFullNameAsync(page, pageSize, fullName);  

//            // Assert
//            Assert.That(actualResult, Is.EqualTo(expectResult));    

//        }

//        /// <summary>
//        /// Test hàm lấy ra số nhân viên trong database
//        /// </summary>
//        /// <returns></returns>
//        [Test]
//        public async Task GetNumEmployeesAsync_GetSuccess_ReturnNumEmployees()
//        {
//            // Arrange
//            // Giả lập kết quả truy vấn database trả về số lượng nhân viên là 50
//            EmployeeRepository.GetNumEmployeesAsync().Returns(50);
//            // Act

//            var actualResult = await EmployeeService.GetNumEmployeesAsync();
//            // Assert

//            Assert.That(actualResult, Is.EqualTo(50));
//        }
//        #endregion

//        #region Test hàm InsertAsync
//        /// <summary>
//        /// Hàm test hàm IsertAsync một nhân viên mới, kiểm tra việc gán Id mới cho nhân viên
//        /// </summary>
//        /// <returns></returns>
//        /// Created by: nkmdang (25/09/2023)
//        [Test]
//        public async Task InsertAsync_EmptyEmployeeId_ReturnIdNotEmpty()
//        {
//            // Arrange
//            var employee = new Employee()
//            {
//                EmployeeId = Guid.Empty,    
//            };
//            var employeeCreateDTO = new EmployeeCreateDTO();
            
            
//            EmployeeService.MapCreateDTOToEntity(employeeCreateDTO).Returns(employee);    

//            // Act
//            var employeeDTO = await EmployeeService.InsertAsync(employeeCreateDTO);

//            // Assert
//            Assert.That(employee.EmployeeId, Is.Not.EqualTo(Guid.Empty));
           
//        }


//        /// <summary>
//        /// Hàm test hàm InsertAsync, gán các thông tin UpdatedBy, ModifiedDate
//        /// </summary>
//        /// <returns></returns>
//        /// Created by: nkmdang (25/09/2023)
//        [Test]
//        public async Task InsertAsync_EmptyAuditNull_ReturnEmployeeAuditNotNull()
            
//        {
//            // Arrange
//            var employee = new Employee()
//            {
//                EmployeeId = Guid.Empty,
//            };
//            var employeeCreateDTO = new EmployeeCreateDTO();
//            EmployeeService.MapCreateDTOToEntity(employeeCreateDTO).Returns(employee);

//            // Act
//            var employeeDTO = await EmployeeService.InsertAsync(employeeCreateDTO);

//            // Assert
//            Assert.That(employee.CreatedBy, Is.EqualTo("NKMDANG"));
//            Assert.That(employee.ModifiedBy, Is.EqualTo("NKMDANG"));
           
//        }


//        /// <summary>
//        /// Hàm test hàm InsertAync, kiểm tra số lần gọi các hàm 
//        /// </summary>
//        /// <returns></returns>
//        /// Created by: nkmdang (25/09/2023)
//        [Test]
//        public async Task InsertAsync_ValidInput_Success()
//        {
//            Guid companyId = Guid.NewGuid();    
//            // Arrange
//            var employee = new Employee()
//            {
//                EmployeeId = Guid.Empty,
//            };
//            var employeeCreateDTO = new EmployeeCreateDTO();
//            EmployeeService.MapCreateDTOToEntity(employeeCreateDTO).Returns(employee);


//            // Act
//            var employeeDTO = await EmployeeService.InsertAsync(employeeCreateDTO);

//            // Assert
//            Assert.That(employee.CreatedBy, Is.EqualTo("NKMDANG"));
//            Assert.That(employee.ModifiedBy, Is.EqualTo("NKMDANG"));
//            await EmployeeService.Received(1).ValidateCreateBusinessAsync(employee, companyId   );
//            await EmployeeRepository.Received(1).InsertAsync(employee);
//        }

//        #endregion

//        #region Test hàm UpdateAsync

//        /// <summary>
//        /// Hàm test hàm UpdateAsync trong trường hợp AuditNull
//        /// </summary>
//        /// <returns></returns>
//        /// Created by: nkmdang (26/09/2023)
//        [Test]
//        public async Task UpdateAsync_AuditNull_ReturnEmployeeAuditNotNull()
//        {
//            // Arrange
//            var id = Guid.NewGuid();
//            var employee = new Employee()
//            {
//                EmployeeId = id
//            };
//            var employeeUpdateDTO = new EmployeeUpdateDTO();  
            
//            EmployeeRepository.GetByIdAsync(id).Returns(employee);  
//            EmployeeService.MapUpdateDTOToEntity(employeeUpdateDTO, employee).Returns(employee);
               
//            // Act
//            var employeeDTO = await EmployeeService.UpdateAsync(employee.EmployeeId, employeeUpdateDTO);
//            // Assert
//            Assert.That(employee.ModifiedBy, Is.EqualTo("NKMDANG"));
//        }

//        /// <summary>
//        /// Hàm test hàm UpdateAsync trong trường hợp thành công
//        /// </summary>
//        /// <returns></returns>
//        /// Created by: nkmdang (26/09/2023)
//        [Test]
//        public async Task UpdateAsync_ValidInput_Success()
//        {
//            // Arrange
//            var id = Guid.NewGuid();
//            var employee = new Employee()
//            {
//                EmployeeId = id
//            };
//            var employeeUpdateDTO = new EmployeeUpdateDTO();
            
//            EmployeeRepository.GetByIdAsync(id).Returns(employee);
//            EmployeeService.MapUpdateDTOToEntity(employeeUpdateDTO, employee).Returns(employee);

//            // Act
//            var employeeDTO = await EmployeeService.UpdateAsync(employee.EmployeeId, employeeUpdateDTO);
//            // Assert
//            Assert.That(employee.ModifiedBy, Is.EqualTo("NKMDANG"));
//            await EmployeeService.Received(1).ValidateUpdateBusinessAsync(employee);
//            await EmployeeRepository.Received(1).UpdateAsync(employee);
//        }
//        #endregion



//        #region Test các hàm Delete
//        /// <summary>
//        /// Hàm test hàm Delete một trường hợp success
//        /// </summary>
//        /// <returns></returns>
//        /// Created by: nkmdang (26/09/2023)
//        [Test]
//        public async Task DeleteAsync_DeleteSuccess_Return1()
//        {
//            // Arrange
//            var employee = new Employee()
//            {
//                EmployeeId = Guid.NewGuid(),
//            };
//           EmployeeRepository.GetByIdAsync(employee.EmployeeId).Returns(employee);
//            EmployeeRepository.DeleteAsync(employee.EmployeeId).Returns(1);
//            // Act
//            var result = await EmployeeService.DeleteAsync(employee.EmployeeId);
//            // Assert
//            Assert.That(result, Is.EqualTo(1));

//            await EmployeeRepository.Received(1).GetByIdAsync(employee.EmployeeId);
//            await EmployeeRepository.Received(1).DeleteAsync(employee.EmployeeId);
//        }


//        /// <summary>
//        /// Hàm test hàm xóa DeleteManyAsync nhiều
//        /// </summary>
//        /// <param name="n">Số bản ghi truyền vào</param>
//        /// <param name="expectedResult">Số bản ghi đã xóa mong muốn nhận được</param>
//        /// <returns></returns>
//        /// Created by: nkmdang (26/09/2023)
//        [TestCase(1, 1)]
//        [TestCase(2, 2)]
//        public async Task DeleteManyAsync_DeleteSuccessAll_ReturnNumEmployeeDeleted(int n, int expectedResult)
//        {
//            // Arrange
//            // Truyền vào 1 list các id và trả về số bản ghi đã xóa đúng với số id đã truyền
//            var employees = new List<Employee>();   
//            var entitiesId = new List<Guid>();
//            for(int i = 0; i < n; i++) 
//            {
//                var employee = new Employee()
//                {
//                    EmployeeId = Guid.NewGuid()
//                };
//                entitiesId.Add(employee.EmployeeId);
//                employees.Add(employee);    
//            }
//            EmployeeRepository.GetByListIdAsync(entitiesId).Returns(employees);
//            EmployeeRepository.DeleteManyAsync(employees).Returns(n);

//            // Act
//            var actualResult = await EmployeeService.DeleteManyAsync(entitiesId);
//            // Assert
//             Assert.That(actualResult, Is.EqualTo(expectedResult));
//             // Hàm GetByListIdAsync ở EmployeeRepository được gọi 1 lần, DeleteManyAsync được gọi 1 lần
//            await EmployeeRepository.Received(1).GetByListIdAsync(entitiesId);
//            await EmployeeRepository.Received(1).DeleteManyAsync(employees);
//        }
//        #endregion


//        [Test]
//        public void MapDTOToEntity_MapSuccess_ReturnEmployee()
//        {
//            string fullName = "FullNameTest";
//            var employeeDTO = new EmployeeDTO()
//            {
//                FullName = fullName
//            };
//            var employee = new Employee()
//            {
//                FullName = fullName
//            };
//            EmployeeMapper.Map<Employee>(employeeDTO).Returns(employee);
//            EmployeeService = new EmployeeService(EmployeeRepository, EmployeeValidate, EmployeeMapper);
//            var actualResult = EmployeeService.MapDTOToEntity(employeeDTO);

//            Assert.That(actualResult, Is.EqualTo(employee));
//        }

//        [Test]
//        public void MapEntityToDTO_MapSuccess_ReturnEmployeeDTO()
//        {
//            string fullName = "FullNameTest";
//            var employeeDTO = new EmployeeDTO()
//            {
//                FullName = fullName
//            };
//            var employee = new Employee()
//            {
//                FullName = fullName
//            };
//            EmployeeMapper.Map<EmployeeDTO>(employee).Returns(employeeDTO);
//            EmployeeService = new EmployeeService(EmployeeRepository, EmployeeValidate, EmployeeMapper);
//            var actualResult = EmployeeService.MapEntityToDTO(employee);

//            Assert.That(actualResult, Is.EqualTo(employeeDTO));
//        }

//        [Test]
//        public void MapUpdateDTOToEntity()
//        {
//            var employeeUpdateDTO = new EmployeeUpdateDTO()
//            {
//                EmployeeCode = "NV-00673",
//                FullName = "Nguyễn Minh Dũng",
//                Gender = Gender.Male,
//                Mobile = "+1 366-729-3627",
//                Address = "3806 W Highland Highway",
//                BankAccount = "51941",
//                BankBranch = "Hà Nội",
//                BankName = "ACB ",
//                PersonalIdentification = "025237871085",
//                PICreatedDate = Convert.ToDateTime("2021-02-19T00:00:00+07:00"),
//                PICreatedPlace = "Hà Nội",
//                Email = "kvijx7116@gmail.com",
 
//                CreatedBy = "Dianne Stack",


//                ModifiedBy = "Porsche Rudolph"
//            };
//            var employee = new Employee() 
//            {
//                EmployeeId = Guid.NewGuid(),    
//            };

//            EmployeeService = new EmployeeService(EmployeeRepository, EmployeeValidate, EmployeeMapper);

//            var result = EmployeeService.MapUpdateDTOToEntity(employeeUpdateDTO, employee);
//            Console.WriteLine(result); 
            
//            Assert.That(result, Is.EqualTo((Employee)employee));    
//        }
//    }
//}
