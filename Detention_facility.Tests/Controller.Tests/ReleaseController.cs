using Detention_facility.Business;
using Detention_facility.Controllers;
using Detention_facility.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace Release_facility.Tests.Controller.Tests
{
    [TestClass]
    public class ReleaseControllerTests
    {
        Release testrelease = new Release()
        {
            ReleaseID = 1,
            DetaineeID = 1,
            DetentionID = 1,
            ReleasedByEmployeeID = 1,
            ReleaseDate = DateTime.Today
        };

        [TestMethod]
        public void TestGetReleaseByID()
        {
            var releaseService = new Mock<IReleaseBusinessLayer>();

            releaseService.Setup(x => x.GetReleaseByID(1)).Returns(testrelease);

            var controller = new ReleaseController(releaseService.Object);

            IHttpActionResult actionResult = controller.GetRelease(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Release>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.ReleaseID);
        }

        [TestMethod]
        public void InsertRelease()
        {
            var releaseService = new Mock<IReleaseBusinessLayer>();

            releaseService.Setup(x => x.InsertRelease(testrelease));
            
            var controller = new ReleaseController(releaseService.Object);

            IHttpActionResult actionResult = controller.InsertRelease(testrelease);
            var contentResult = actionResult as OkNegotiatedContentResult<Release>;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, contentResult.Content.ReleaseID);
        }

        [TestMethod]
        public void UpdateRelease()
        {
            var releaseService = new Mock<IReleaseBusinessLayer>();

            releaseService.Setup(x => x.GetReleaseByID(1)).Returns(testrelease);

            var controller = new ReleaseController(releaseService.Object);

            IHttpActionResult actionResult = controller.UpdateRelease(1, testrelease);
            var contentResult = actionResult as OkNegotiatedContentResult<Release>;

            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.ReleaseID);
        }

        [TestMethod]
        public void GetReleases()
        {
            var releaseService = new Mock<IReleaseBusinessLayer>();

            releaseService.Setup(x => x.GetReleases()).Returns(new List<Release>());

            var controller = new ReleaseController(releaseService.Object);

            IHttpActionResult actionResult = controller.GetReleases();
            var contentResult = actionResult as OkNegotiatedContentResult<List<Release>>;

            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void DeleteRelease()
        {
            var releaseService = new Mock<IReleaseBusinessLayer>();

            releaseService.Setup(x => x.GetReleaseByID(1))
             .Returns(testrelease);

            var controller = new ReleaseController(releaseService.Object);

            IHttpActionResult actionResult = controller.DeleteRelease(1);

            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Release>));
        }
    }
}
