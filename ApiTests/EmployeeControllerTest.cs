/*using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PracticeTesting.Controllers;
using PracticeTesting.Models;
using PracticeTesting.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTests
{
    [TestClass]
    public class EmployeeControllerTest
    {
        private Mock<IEmployee> _iemployeerepo;
        private Fixture _fixture;
        private EmployeeController _controller;

        public EmployeeControllerTest()
        {
            _fixture = new Fixture();
            _iemployeerepo = new Mock<IEmployee>();
            _controller = new EmployeeController(_iemployeerepo.Object);
        }

        [TestMethod]
        public async Task Get_Employee_ReturnOk()
        {
            // Arrange
            var employeelist = _fixture.CreateMany<Employee>(3).ToList();
            _iemployeerepo.Setup(repo => repo.GetAll()).ReturnsAsync(employeelist);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(employeelist.Count, result.Count);
            CollectionAssert.AreEquivalent(employeelist, result);
        }

        [TestMethod]
        public async Task post_employee()
        {
            var employee = _fixture.Create<Employee>();
            _iemployeerepo.Setup(repo => repo.PostAsync(It.IsAny<Employee>())).ReturnsAsync(employee);
            _controller=new EmployeeController(_iemployeerepo.Object);

            var result = await _controller.Post(employee);

            Assert.IsNotNull(result);
            var OkResult = result as OkObjectResult;
            Assert.IsNotNull(OkResult);
            Assert.AreEqual(200, OkResult.StatusCode);

        }

        [TestMethod]
        public async Task put_employee()
        {
            // Arrange
            var id = Guid.NewGuid(); // Generate a new GUID for the employee ID
            var employee = _fixture.Create<Employee>();
            _iemployeerepo.Setup(repo => repo.UpdateAsync(id, It.IsAny<Employee>())).ReturnsAsync(employee);
            _controller = new EmployeeController(_iemployeerepo.Object);

            // Act
            var result = await _controller.Put(id, employee);

            Assert.IsNotNull(result);
            var OkResult = result as OkObjectResult;
            Assert.IsNotNull(OkResult);
            Assert.AreEqual(200, OkResult.StatusCode);

        }

        [TestMethod]

       
        public async Task Delete_Employee_ReturnsNotFoundResult()
        {
            // Arrange
            var id = Guid.NewGuid(); // Generate a new GUID for the employee ID
            var deletedEmployee = new Employee {id = id, EmployeeName = "Test Employee", EmployeeCity = "Test City", EmployeeDepartment = "Test Department" };
            _iemployeerepo.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(deletedEmployee); // Return null to simulate not finding the employee
            _controller = new EmployeeController(_iemployeerepo.Object);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsNotNull(result, "Result is null"); // Check if the result is null
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "Result is not OkObjectResult"); // Check if result is OkObjectResult
            Assert.AreEqual(200, okResult.StatusCode, "Status code is not 200"); // Verify status code
            Assert.IsNotNull(okResult.Value, "Value in OkObjectResult is null"); // Check if value in OkObjectResult is null
            Assert.AreEqual(deletedEmployee, okResult.Value, "Returned employee does not match deleted employee"); // Verify that returned employee matches deleted employee
        }

        [TestMethod]
        public async Task GetById()
        {
            //arrange
            var id = new Guid();
            var employee = _fixture.Create<Employee>();
            _iemployeerepo.Setup(repo=>repo.GetById(id)).ReturnsAsync(employee);
            _controller= new EmployeeController(_iemployeerepo.Object);

            //act
            var result=await _controller.GetById(id);

            //assert

            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

    }
}

*/
