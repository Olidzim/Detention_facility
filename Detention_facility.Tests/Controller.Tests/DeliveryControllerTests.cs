using Detention_facility.Business;
using Detention_facility.Controllers;
using Detention_facility.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace Delivery_facility.Tests.Controller.Tests
{
    [TestClass]
    public class DeliveryControllerTests
    {
        Delivery testdelivery = new Delivery()
        {
            DeliveryID = 1,
            DetaineeID = 1,
            DetentionID = 1,
            PlaceAddress = "Place",
            DeliveredByEmployeeID = 1,
            DeliveryDate = DateTime.Today
        };

        [TestMethod]
        public void TestGetDeliveryByID()
        {
            var deliveryService = new Mock<IDeliveryBusinessLayer>();

            deliveryService.Setup(x => x.GetDeliveryByID(1)).Returns(testdelivery);

            var controller = new DeliveryController(deliveryService.Object);

            IHttpActionResult actionResult = controller.GetDelivery(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Delivery>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.DeliveryID);
        }

        [TestMethod]
        public void InsertDelivery()
        {
            var deliveryService = new Mock<IDeliveryBusinessLayer>();

            deliveryService.Setup(x => x.InsertDelivery(testdelivery));

            var controller = new DeliveryController(deliveryService.Object);

            IHttpActionResult actionResult = controller.InsertDelivery(testdelivery);
            var contentResult = actionResult as OkNegotiatedContentResult<Delivery>;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, contentResult.Content.DeliveryID);
        }

        [TestMethod]
        public void UpdateDelivery()
        {
            var deliveryService = new Mock<IDeliveryBusinessLayer>();

            deliveryService.Setup(x => x.GetDeliveryByID(1)).Returns(testdelivery);

            var controller = new DeliveryController(deliveryService.Object);

            IHttpActionResult actionResult = controller.UpdateDelivery(1, testdelivery);
            var contentResult = actionResult as OkNegotiatedContentResult<Delivery>;

            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.DeliveryID);
        }

        [TestMethod]
        public void GetDeliverys()
        {
            var deliveryService = new Mock<IDeliveryBusinessLayer>();

            deliveryService.Setup(x => x.GetDeliveries()).Returns(new List<Delivery>());

            var controller = new DeliveryController(deliveryService.Object);

            IHttpActionResult actionResult = controller.GetDeliveries();
            var contentResult = actionResult as OkNegotiatedContentResult<List<Delivery>>;

            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void DeleteDelivery()
        {
            var deliveryService = new Mock<IDeliveryBusinessLayer>();

            deliveryService.Setup(x => x.GetDeliveryByID(1))
             .Returns(testdelivery);

            var controller = new DeliveryController(deliveryService.Object);

            IHttpActionResult actionResult = controller.DeleteDelivery(1);

            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Delivery>));
        }
    }
}
