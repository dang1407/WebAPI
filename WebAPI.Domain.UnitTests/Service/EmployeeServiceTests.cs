//using WebAPI.Application;
//using NSubstitute;
//using NUnit.Framework;

//namespace WebAPI.Domain.UnitTests
//{
//    [TestFixture]
//    public class EmployeeValidateTests
//    {
//        public IEmployeeRepository EmployeeRepository { get; set; }
//        public IDepartmentValidate DepartmentValidate { get; set; }

//        public IEmployeeValidate EmployeeValidate { get; set; }


//        [SetUp]
//        public void SetUp()
//        {
//            EmployeeRepository = Substitute.For<IEmployeeRepository>();
//            DepartmentValidate = Substitute.For<IDepartmentValidate>();
//            EmployeeValidate = Substitute.For<EmployeeValidate>(EmployeeRepository, DepartmentValidate);
//        }

//        /// <summary>
//        /// Hàm test hàm CheckEmployeeExistAsync trong trượng nhân viên không tồn tại
//        /// </summary>
//        /// <returns></returns>
//        [Test]
//        public async Task CheckEmployeeExistAsync_NotExistEmployee_Success()
//        {

//            // Arrange
//            var employee = new Employee()
//            {
//                EmployeeCode = "NV-Test"
//            };
//            EmployeeRepository.IsExistEmployeeAsync(employee.EmployeeCode).Returns(false);
//            // Act
//            await EmployeeValidate.CheckEmployeeExistAsync(employee);

//            // Assert

//            await EmployeeRepository.Received(1).IsExistEmployeeAsync(employee.EmployeeCode);
//        }

//        /// <summary>
//        /// Hàm test hàm CheckEmployeeExistAsync trong trượng nhân viên tồn tại
//        /// </summary>
//        /// <returns></returns>
//        [Test]
//        public async Task CheckEmployeeExistAsync_ExistEmployee_ThrowConflictException()
//        {

//            // Arrange
//            var employee = new Employee()
//            {
//                EmployeeCode = "NV-Test"
//            };

//            EmployeeRepository.IsExistEmployeeAsync(employee.EmployeeCode).Returns(true);
//            // Act
//            var exception = Assert.ThrowsAsync<ConflictException>(async () => await EmployeeValidate.CheckEmployeeExistAsync(employee));

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.That(exception.DevMessage, Is.EqualTo("Mã nhân viên <NV-Test> đã tồn tại trong hệ thống, vui lòng kiểm tra lại."));
//                Assert.That(exception.UserMessage, Is.EqualTo("Mã nhân viên <NV-Test> đã tồn tại trong hệ thống, vui lòng kiểm tra lại."));
//                Assert.That(exception.ErrorCode, Is.EqualTo((int)EmployeeEnum.EmployeeCodeExistErrorCode));
//            });
//            await EmployeeRepository.Received(1).IsExistEmployeeAsync(employee.EmployeeCode);

//        }
//    }
//}
