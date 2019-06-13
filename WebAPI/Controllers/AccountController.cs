using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Detention_facility.Business;
using Detention_facility.Models;

namespace Detention_facility.Controllers
{
    public class AccountController : ApiController
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IHttpActionResult RegisterUser([FromBody] User user)
        {          
            if (ModelState.IsValid)
            {
                _accountService.RegisterUser(user);
                return Ok(user);
            }
            CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IHttpActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (_accountService.GetUserByID(id) == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет такого пользователя");
                return NotFound();
            }          
            if (ModelState.IsValid)
            {
                _accountService.UpdateUser(id, user);
                return Ok(user);
            }
            CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpPut]
        public IHttpActionResult UpdateUserPassword (int id, [FromBody] string password)
        {
            if (_accountService.GetUserByID(id) == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет такого пользователя");
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _accountService.UpdateUserPassword(id, password);
                return Ok();
            }
            CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            if (_accountService.GetUserByID(id) == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет такого пользователя");
                return NotFound();
            }
            _accountService.DeleteUser(id);
            return Ok();
        }
    }
}
