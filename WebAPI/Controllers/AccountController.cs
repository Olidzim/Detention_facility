using Detention_facility.Business;
using Detention_facility.Models;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WebAPI.Models;

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
        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {
            var user = _accountService.GetUserByID(id);
            if (user == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет такого пользователя");
                return NotFound();
            }
            return Ok(user);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IHttpActionResult RegisterUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
                return BadRequest(ModelState);
            }
            _accountService.RegisterUser(user);
            return Ok(user);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            var users_list = _accountService.GetUsers();
            if (users_list == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Не существует доставки с таким номером");
                return NotFound();
            }
            return Ok(users_list);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IHttpActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
                return BadRequest(ModelState);
                
            }

            if (_accountService.GetUserByID(id) == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет такого пользователя");
                return NotFound();
            }

            _accountService.UpdateUser(id, user);
            return Ok(user);
        }


        [Authorize(Roles = "Admin,Editor")]
        [HttpPut]
        public IHttpActionResult UpdateUserPassword(int id, [FromBody] string password)
        {
            var user = _accountService.GetUserByID(id);
            if (!ModelState.IsValid)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
                return BadRequest(ModelState);            
            }
            if (user == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет такого пользователя");
                return NotFound();
            }
            _accountService.UpdateUserPassword(id, password);
            return Ok(user);
        }


        [Authorize(Roles = "Admin,Editor")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            var userForDelete = _accountService.GetUserByID(id);
            if (userForDelete == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет такого пользователя");
                return NotFound();
            }
            _accountService.DeleteUser(id);
            return Ok(userForDelete);
        }


        [Authorize(Roles = "Admin,Editor,User")]
        [HttpGet]
        public IHttpActionResult GetRole()
        {
            return Ok(Request.GetOwinContext().Authentication.User.Claims.First(claim => claim.Type == ClaimsIdentity.DefaultRoleClaimType).Value);
        }

    }
}
