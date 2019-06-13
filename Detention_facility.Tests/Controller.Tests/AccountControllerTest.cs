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
    public class AccountControllerTests
    {
        User testuser = new User()
        {
            UserID = 1,
            Login = "FirstName",
            Password = "LastName",
            Email = "Patronymic",
            Role = "Position"
        };      

        [TestMethod]
        public void InsertUser()
        {
            var userService = new Mock<IAccountService>();

            userService.Setup(x => x.RegisterUser(testuser));
            userService.Setup(x => x.GetUserByID(1)).Returns(new User { });

            var controller = new AccountController(userService.Object);

            IHttpActionResult actionResult = controller.RegisterUser(testuser);
            var contentResult = actionResult as OkNegotiatedContentResult<User>;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, contentResult.Content.UserID);
        }


        [TestMethod]
        public void UpdateUser()
        {
            var userService = new Mock<IAccountService>();

            userService.Setup(x => x.GetUserByID(1)).Returns(testuser);

            var controller = new AccountController(userService.Object);

            IHttpActionResult actionResult = controller.UpdateUser(1, testuser);
            var contentResult = actionResult as OkNegotiatedContentResult<User>;

            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.UserID);
        }

        [TestMethod]
        public void UpdateUserPassword()
        {
            var userService = new Mock<IAccountService>();

            userService.Setup(x => x.GetUserByID(1)).Returns(testuser);

            var controller = new AccountController(userService.Object);

            IHttpActionResult actionResult = controller.UpdateUserPassword(1, "Password");
            var contentResult = actionResult as OkNegotiatedContentResult<User>;

            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.UserID);
        }

        [TestMethod]
        public void DeleteUser()
        {
            var userService = new Mock<IAccountService>();

            userService.Setup(x => x.GetUserByID(1)).Returns(testuser);

            var controller = new AccountController(userService.Object);

            IHttpActionResult actionResult = controller.DeleteUser(1);

            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<User>));
        }
    }
}
