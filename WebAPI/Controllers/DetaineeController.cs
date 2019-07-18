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
        public IHttpActionResult InsertDetainee([FromBody] Detainee detainee)
        {
            if (ModelState.IsValid)
            {
                detainee.DetaineeID = _detaineeService.InsertDetainee(detainee);               
                return Ok(detainee);
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
        public IHttpActionResult UpdateDetainee(int id, [FromBody] Detainee detainee)
        {
            if (ModelState.IsValid)
            {
                _detaineeService.UpdateDetainee(id, detainee);
                return Ok(detainee);
            }
            CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
            return BadRequest(ModelState);
        }
        
        [HttpGet]
        public IHttpActionResult GetDetaineeByID(int id)
        {
            var detainee = _detaineeCachingService.Get(id);
            if (detainee == null)
            {
                detainee = _detaineeService.GetDetaineeByID(id); if (detainee == null)
                {
                    CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Такой задержанный отсутствует в базе данных");
                    return NotFound();
                }
                _detaineeCachingService.Add(detainee);
            }
            if (detainee == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Такой задержанный отсутствует в базе данных");
                return NotFound();
            }
            return Ok(detainee);
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
            var detaineesList = _detaineeService.GetDetainees();
            if (detaineesList == null)
            {
                return NotFound();
            }
            return Ok(detaineesList);
        }

        [HttpGet]
        public IHttpActionResult GetDetaineeByDetentionID(int id)
        {
            var detaineesList = _detaineeService.GetDetaineesByDetentionID(id);
            if (detaineesList == null)
            {
                return NotFound();
            }
            return Ok(detaineesList);
        }

        [HttpGet]
        public IHttpActionResult GetDet(string term)
        {
            var detainee_list = _detaineeService.Detainees(term);
            if (detainee_list == null)
            {
                return NotFound();
            }
            return Ok(detainee_list);
        }

        [HttpGet]
        public IHttpActionResult GetDetaineeByAddress(string term)
        {
            var detainee_list = _detaineeService.GetDetaineesByAddres(term);
            if (detainee_list == null)
            {
                return NotFound();
            }
            return Ok(detainee_list);
        }
    }
}
