using Detention_facility.Business;
using Detention_facility.Controllers;
using Detention_facility.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace Detention_facility.Tests.Controller.Tests
{
    [TestClass]
    public class DetentionControllerTests
    {
        Detention testdetention = new Detention()
        {
            DetentionID = 1,
            DetentionDate = DateTime.Today,
            DetainedByEmployeeID = 1    
        };   

        [TestMethod]
        public void TestGetDetentionByID()
        {
            var employeeService = new Mock<IEmployeeBusinesslayer>();
            var detentionService = new Mock<IDetentionBusinessLayer>();

            detentionService.Setup(x => x.GetDetentionByID(1)).Returns(testdetention);  
            
            var controller = new DetentionController(detentionService.Object, employeeService.Object);

            IHttpActionResult actionResult = controller.GetDetention(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Detention>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.DetentionID);
        }

        [TestMethod]
        public void InsertDetention()
        {
            var employeeService = new Mock<IEmployeeBusinesslayer>();
            var detentionService = new Mock<IDetentionBusinessLayer>();

            detentionService.Setup(x => x.InsertDetention(testdetention));
            employeeService.Setup(x => x.GetEmployeeByID(1)).Returns(new Employee { });

            var controller = new DetentionController(detentionService.Object, employeeService.Object);

            IHttpActionResult actionResult = controller.InsertDetention(testdetention);
            var contentResult = actionResult as OkNegotiatedContentResult<Detention>;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, contentResult.Content.DetentionID);
        }

        [TestMethod]
        public void TestGetDetentionByDate()
        {
            var employeeService = new Mock<IEmployeeBusinesslayer>();
            var detentionService = new Mock<IDetentionBusinessLayer>();

            List<Detention> testDetentionList = new List<Detention>();
            testDetentionList.Add(testdetention);

            detentionService.Setup(x => x.GetDetentionsByDate(DateTime.Today)).Returns(testDetentionList);

            var controller = new DetentionController(detentionService.Object, employeeService.Object);

            IHttpActionResult actionResult = controller.GetDetentionsByDate(DateTime.Today);
            var contentResult = actionResult as OkNegotiatedContentResult<List<Detention>>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void TestGetDetentionByLastName()
        {
            var employeeService = new Mock<IEmployeeBusinesslayer>();
            var detentionService = new Mock<IDetentionBusinessLayer>();

            List<Detention> testDetentionList = new List<Detention>();
            testDetentionList.Add(testdetention);

            detentionService.Setup(x => x.GetDetentionsByLastName("Hello")).Returns(testDetentionList);

            var controller = new DetentionController(detentionService.Object, employeeService.Object);

            IHttpActionResult actionResult = controller.GetDetentionsByLastName("Hello");
            var contentResult = actionResult as OkNegotiatedContentResult<List<Detention>>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);  

        }

        [TestMethod]
        public void TestGetDetentionByPlace()
        {
            var employeeService = new Mock<IEmployeeBusinesslayer>();
            var detentionService = new Mock<IDetentionBusinessLayer>();

            List<Detention> testDetentionList = new List<Detention>();
            testDetentionList.Add(testdetention);

            detentionService.Setup(x => x.GetDetentionsByPlace("Place")).Returns(testDetentionList);

            var controller = new DetentionController(detentionService.Object, employeeService.Object);

            IHttpActionResult actionResult = controller.GetDetentionsByPlace("Place");
            var contentResult = actionResult as OkNegotiatedContentResult<List<Detention>>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }


        [TestMethod]
        public void UpdateDetention()
        {
            var employeeService = new Mock<IEmployeeBusinesslayer>();
            var detentionService = new Mock<IDetentionBusinessLayer>();

            detentionService.Setup(x => x.GetDetentionByID(1)).Returns(testdetention);
            employeeService.Setup(x => x.GetEmployeeByID(1)).Returns(new Employee { });

            var controller = new DetentionController(detentionService.Object, employeeService.Object);

            IHttpActionResult actionResult = controller.UpdateDetention(1, testdetention);
            var contentResult = actionResult as OkNegotiatedContentResult<Detention>;

            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.DetentionID);
        }

        [TestMethod]
        public void GetDetentions()
        {
            var employeeService = new Mock<IEmployeeBusinesslayer>();
            var detentionService = new Mock<IDetentionBusinessLayer>();

            detentionService.Setup(x => x.GetDetentions()).Returns(new List<Detention>());

            var controller = new DetentionController(detentionService.Object, employeeService.Object);

            IHttpActionResult actionResult = controller.GetDetentions();
            var contentResult = actionResult as OkNegotiatedContentResult<List<Detention>>;

            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void DeleteDetention()
        {
            var employeeService = new Mock<IEmployeeBusinesslayer>();
            var detentionService = new Mock<IDetentionBusinessLayer>();

            detentionService.Setup(x => x.GetDetentionByID(1))
             .Returns(testdetention);

            var controller = new DetentionController(detentionService.Object, employeeService.Object);

            IHttpActionResult actionResult = controller.DeleteDetention(1);

            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Detention>));
        }
    }
}
