using Detention_facility.Business;
using Detention_facility.Controllers;
using Detention_facility.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace Detention_facility.Tests.Controller.Tests
{
    [TestClass]
    public class EmployeeControllerTests
    {
        Employee testemployee = new Employee()
        {
            EmployeeID = 1,
            FirstName = "FirstName",
            LastName = "LastName",
            Patronymic = "Patronymic",
            Position = "Position",
            EmployeeRank = "Rank"

        };

        [TestMethod]
        public void TestGetEmployeeByID()
        {
            var employeeService = new Mock<IEmployeeBusinesslayer>();

            employeeService.Setup(x => x.GetEmployeeByID(1)).Returns(testemployee);

            var controller = new EmployeeController(employeeService.Object);

            IHttpActionResult actionResult = controller.GetEmployee(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Employee>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.EmployeeID);
        }

        [TestMethod]
        public void InsertEmployee()
        {
            var employeeService = new Mock<IEmployeeBusinesslayer>();

            employeeService.Setup(x => x.InsertEmployee(testemployee));
            employeeService.Setup(x => x.GetEmployeeByID(1)).Returns(new Employee { });

            var controller = new EmployeeController(employeeService.Object);

            IHttpActionResult actionResult = controller.InsertEmployee(testemployee);
            var contentResult = actionResult as OkNegotiatedContentResult<Employee>;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, contentResult.Content.EmployeeID);
        }


        [TestMethod]
        public void UpdateEmployee()
        {
            var employeeService = new Mock<IEmployeeBusinesslayer>();

            employeeService.Setup(x => x.GetEmployeeByID(1)).Returns(testemployee);

            var controller = new EmployeeController(employeeService.Object);

            IHttpActionResult actionResult = controller.UpdateEmployee(1, testemployee);
            var contentResult = actionResult as OkNegotiatedContentResult<Employee>;

            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.EmployeeID);
        }

        [TestMethod]
        public void GetEmployees()
        {
            var employeeService = new Mock<IEmployeeBusinesslayer>();

            employeeService.Setup(x => x.GetEmployees())
               .Returns(new List<Employee>());

            var controller = new EmployeeController(employeeService.Object);

            IHttpActionResult actionResult = controller.GetEmployees();
            var contentResult = actionResult as OkNegotiatedContentResult<List<Employee>>;

            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void DeleteEmployee()
        {
            var employeeService = new Mock<IEmployeeBusinesslayer>();

            employeeService.Setup(x => x.GetEmployeeByID(1)).Returns(testemployee);

            var controller = new EmployeeController(employeeService.Object);

            IHttpActionResult actionResult = controller.DeleteEmployee(1);

            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Employee>));
        }
    }
}
