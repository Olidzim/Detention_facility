using Detention_facility.Business;
using Detention_facility.Controllers;
using Detention_facility.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http;
using System.Web.Http.Results;

namespace Detention_facility.Tests.Controller.Tests
{
    [TestClass]
    public class DetaineeControllerTests
    {
        [TestMethod]
        public void TestGetDetaineeByID()
        {
            var mockService = new Mock<IDetaineeBusinessLayer>();
            mockService.Setup(x => x.GetDetaineeByID(1))
                .Returns(new Detainee { DetaineeID = 1 });

            var controller = new DetaineeController(mockService.Object);

            IHttpActionResult actionResult = controller.GetDetaineeByID(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Detainee>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.DetaineeID);
        }            
    }
}
