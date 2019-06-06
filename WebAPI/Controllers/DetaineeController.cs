using Detention_facility.Business;
using Detention_facility.Models;
using System.Collections.Generic;
using System.Web.Http;


namespace Detention_facility.Controllers
{
    public class DetaineeController : ApiController
    {    
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

                return Ok(Detainee);
            }
            CustomLogging.LogMessage(CustomLogging.TracingLevel.ERROR, CustomLogging.ModelStatusConverter(ModelState));
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
            CustomLogging.LogMessage(CustomLogging.TracingLevel.ERROR, CustomLogging.ModelStatusConverter(ModelState));
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
