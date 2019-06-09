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
    public class DetaineeControllerTests
    {
        Detainee testdetainee = new Detainee()
        {
            DetaineeID = 1,
            FirstName = "FirstName",
            LastName = "LastName",
            Patronymic = "Patronymic",
            MaritalStatus = "Single",
            Job = "No job",
            MobilePhoneNumber = "No",
            HomePhoneNumber = "No",
            Photo = "Photo",
            ExtraInfo = "ExtraInfo",
            ResidencePlace = "Place"
        };
        [TestMethod]
        public void TestGetDetaineeByID()
        {
            var detaineeService = new Mock<IDetaineeBusinessLayer>();
            detaineeService.Setup(x => x.GetDetaineeByID(1))
                .Returns(testdetainee);
            var detaineeCachingService = new Mock<IDetaineeCachingService>();

            var controller = new DetaineeController(detaineeService.Object, detaineeCachingService.Object);

            IHttpActionResult actionResult = controller.GetDetaineeByID(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Detainee>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.DetaineeID);
        }

        [TestMethod]
        public void InsertDetainee()
        {
            var detaineeService = new Mock<IDetaineeBusinessLayer>();
            detaineeService.Setup(x => x.InsertDetainee(testdetainee));
          
            var detaineeCachingService = new Mock<IDetaineeCachingService>();

            var controller = new DetaineeController(detaineeService.Object, detaineeCachingService.Object);

            IHttpActionResult actionResult = controller.InsertDetainee(testdetainee);
            var contentResult = actionResult as OkNegotiatedContentResult<Detainee>;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, contentResult.Content.DetaineeID);
        }

        [TestMethod]
        public void AddDetaineeToDetention()
        {
            string message = null;
            var detaineeService = new Mock<IDetaineeBusinessLayer>();
            detaineeService.Setup(x => x.CheckValuesForAddDetainee(1, 1)).Returns(message);
            detaineeService.Setup(x => x.AddDetaineeToDetention(1, 1));

            var detaineeCachingService = new Mock<IDetaineeCachingService>();

            var controller = new DetaineeController(detaineeService.Object, detaineeCachingService.Object);

            IHttpActionResult actionResult = controller.AddDetaineeToDetention(1,1);
            var contentResult = actionResult as OkNegotiatedContentResult<String>;
            Assert.IsNotNull(contentResult.Content);

        }


        [TestMethod]
        public void UpdateDetainee()
        {
            var detaineeService = new Mock<IDetaineeBusinessLayer>();
           // detaineeService.Setup(x => x.UpdateDetainee(1,testdetainee));

            var detaineeCachingService = new Mock<IDetaineeCachingService>();

            var controller = new DetaineeController(detaineeService.Object, detaineeCachingService.Object);

            IHttpActionResult actionResult = controller.UpdateDetainee(1,testdetainee);
            var contentResult = actionResult as OkNegotiatedContentResult<Detainee>;

            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.DetaineeID);
        }

        [TestMethod]
        public void GetDetainees()
        {
            var detaineeService = new Mock<IDetaineeBusinessLayer>();
            detaineeService.Setup(x => x.GetDetainees())
               .Returns(new List<Detainee>());

            var detaineeCachingService = new Mock<IDetaineeCachingService>();

            var controller = new DetaineeController(detaineeService.Object, detaineeCachingService.Object);

            IHttpActionResult actionResult = controller.GetDetainees();
            var contentResult = actionResult as OkNegotiatedContentResult<List<Detainee>>;

            Assert.IsNotNull(contentResult.Content);           
        }

        [TestMethod]
        public void DeleteDetainee()
        {
            var detaineeService = new Mock<IDetaineeBusinessLayer>();
            detaineeService.Setup(x => x.GetDetaineeByID(1))
             .Returns(testdetainee);

            var detaineeCachingService = new Mock<IDetaineeCachingService>();

            var controller = new DetaineeController(detaineeService.Object, detaineeCachingService.Object);

            IHttpActionResult actionResult = controller.DeleteDetainee(1);

            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }
    }
}
