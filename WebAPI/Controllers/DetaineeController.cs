using Detention_facility.Business;
using Detention_facility.Models;
using System.Collections.Generic;
using System.Web.Http;
//using NLog;
using System;
using System.Net;
using Newtonsoft.Json;
using System.Linq;

namespace Detention_facility.Controllers
{
    public class DetaineeController : ApiController
    {
       // private static Logger logger = LogManager.GetCurrentClassLogger();

        private IDetaineeBusinessLayer _detaineeService;

        public DetaineeController(IDetaineeBusinessLayer detaineeService)
        {
            _detaineeService = detaineeService;
        }

        [HttpPost]
        public IHttpActionResult InsertDetainee([FromBody] Detainee Detainee)
        {
            if (ModelState.IsValid)
            {
                _detaineeService.InsertDetainee(Detainee);
               // logger.Info("New detainee added" + Environment.NewLine + DateTime.Now);
                return Ok(Detainee);
            }
            
          /*  string errors = JsonConvert.SerializeObject(ModelState.Values
    .SelectMany(state => state.Errors)
    .Select(error => error.ErrorMessage));
            logger.Info(errors + Environment.NewLine + DateTime.Now);*/
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IHttpActionResult UpdateDetainee(int id, [FromBody] Detainee Detainee)
        {
            if (ModelState.IsValid)
            {
                _detaineeService.UpdateDetainee(id, Detainee);
                return Ok(Detainee);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public IHttpActionResult GetDetaineeByID(int id)
        {
            var Detainee = _detaineeService.GetDetaineeByID(id);
            if (Detainee == null)
            {
                return NotFound();
            }
            return Ok(Detainee);
        }

        [HttpDelete]
        public void DeleteDetainee(int id)
        {
            _detaineeService.DeleteDetainee(id);
        }

        [HttpGet]
        public IHttpActionResult GetDetainees()
        {
            List<Detainee> Detainees_list = _detaineeService.GetDetainees();
            if (Detainees_list == null)
            {
                return NotFound();
            }
            return Ok(Detainees_list);
        }

        [HttpGet]
        public IHttpActionResult GetDetaineeByDetentionID(int id)
        {
            List<Detainee> Detainees_list = _detaineeService.GetDetaineesByDetentionID(id);
            if (Detainees_list == null)
            {
                return NotFound();
            }
            return Ok(Detainees_list);
        }
    }
}
