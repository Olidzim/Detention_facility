using Detention_facility.Business;
using Detention_facility.Models;
using System.Collections.Generic;
using System.Web.Http;


namespace Detention_facility.Controllers
{
    public class DetaineeController : ApiController
    {

        private IDetaineeBusinessLayer _detaineeService;       
        private IDetaineeCachingService _detaineeCachingService;

        public DetaineeController(IDetaineeBusinessLayer detaineeService, IDetaineeCachingService detaineeCachingService)
        {
            _detaineeService = detaineeService;
            _detaineeCachingService = detaineeCachingService;            
        }

        [Authorize(Roles ="Admin,Editor")] 
        [HttpPost]
        public IHttpActionResult InsertDetainee([FromBody] Detainee Detainee)
        {
            if (ModelState.IsValid)
            {
                _detaineeService.InsertDetainee(Detainee);

                return Ok(Detainee);
            }
            CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
            return BadRequest(ModelState);
        }

        [Authorize(Roles ="Admin,Editor")] 
        [HttpPost]
        public IHttpActionResult AddDetaineeToDetention(int id, [FromBody] int detentionID)
        {
            if (_detaineeService.CheckValuesForAddDetainee(id, detentionID) != null)
            { 
                return BadRequest(_detaineeService.CheckValuesForAddDetainee(id, detentionID));
            }
            _detaineeService.AddDetaineeToDetention(id, detentionID);
            return Ok("Задержанный добавлен к задержанию");
        }

        [Authorize(Roles ="Admin,Editor")] 
        [HttpPut]
        public IHttpActionResult UpdateDetainee(int id, [FromBody] Detainee Detainee)
        {
            if (ModelState.IsValid)
            {
                _detaineeService.UpdateDetainee(id, Detainee);
                return Ok(Detainee);
            }
            CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
            return BadRequest(ModelState);
        }
        
        [HttpGet]
        public IHttpActionResult GetDetaineeByID(int id)
        {
            var Detainee = _detaineeCachingService.Get(id);

            if (Detainee == null)
            {
                Detainee = _detaineeService.GetDetaineeByID(id);
                _detaineeCachingService.Add(Detainee);
            }
            if (Detainee == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Такой задержанный отсутствует в базе данных");
                return NotFound();
            }
            return Ok(Detainee);
        }

        [Authorize(Roles ="Admin,Editor")] 
        [HttpDelete]
        public IHttpActionResult DeleteDetainee(int id)           
        {
            var detaineeForDelete = _detaineeService.GetDetaineeByID(id);
            if (detaineeForDelete == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Такой задержанный отсутствует в базе данных");
                return NotFound();
            }
            else
            {
                _detaineeService.DeleteDetainee(id);
                return Ok(detaineeForDelete);
            }
        }

        [Authorize(Roles ="Admin,Editor")] 
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
