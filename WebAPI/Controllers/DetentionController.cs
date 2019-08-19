using Detention_facility.Business;
using Detention_facility.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Detention_facility.Controllers
{
    public class DetentionController : ApiController
    {
        private IDetentionBusinessLayer _detentionService;
        private IEmployeeBusinesslayer _employeeService;


        public DetentionController(IDetentionBusinessLayer detentionService, IEmployeeBusinesslayer employeeService)
        {
            _detentionService = detentionService;
            _employeeService = employeeService;
        }


        [Authorize(Roles = "Admin,Editor,User")]
        [HttpGet]
        public IHttpActionResult GetDetention(int id)
        {
            var detention = _detentionService.GetDetentionByID(id);
            if (detention == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Не такого задержания");
                return NotFound();
            }
            return Ok(detention);
        }


        [Authorize(Roles = "Admin,Editor,User")]
        [HttpGet]
        public IHttpActionResult GetDetentions()
        {
            var detentionsList = _detentionService.GetDetentions();
            if (detentionsList == null)
            {
                return NotFound();
            }
            return Ok(detentionsList);
        }


        [Authorize(Roles = "Admin,Editor,User")]
        [HttpGet]
        public IHttpActionResult GetSmartDetentionsByDetaineeID(int id)
        {
            var detentionsList = _detentionService.GetSmartDetentionsByDetaineeID(id);
            if (detentionsList == null)
            {
                return NotFound();
            }
            return Ok(detentionsList);
        }


        [Authorize(Roles = "Admin,Editor,User")]
        [HttpGet]
        public IHttpActionResult GetSmartDetentionsByDetentionID(int id)
        {
            var detentionsList = _detentionService.GetSmartDetentionsByDetentionID(id);
            if (detentionsList == null)
            {
                return NotFound();
            }
            return Ok(detentionsList);
        }


        [Authorize(Roles = "Admin,Editor,User")]
        [HttpGet]
        public IHttpActionResult GetSmartDetentions()
        {
            var detentionsList = _detentionService.GetSmartDetentions();
            if (detentionsList == null)
            {
                return NotFound();
            }
            return Ok(detentionsList);
        }


        [Authorize(Roles = "Admin,Editor,User")]
        [HttpPost]
        public IHttpActionResult GetDetentionsByPlace([FromBody] string place)
        {
            var detentionsList = _detentionService.GetDetentionsByPlace(place);
            if (detentionsList == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Не таких задержания");
                return NotFound();
            }
            return Ok(detentionsList);
        }


        [Authorize(Roles = "Admin,Editor,User")]
        [HttpGet]
        public IHttpActionResult GetDetentionsByLastName([FromBody] string lastname)
        {
            var detentionsList = _detentionService.GetDetentionsByLastName(lastname);
            if (detentionsList == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Не таких задержания");
                return NotFound();
            }
            return Ok(detentionsList);
        }


        [Authorize(Roles = "Admin,Editor,User")]
        [HttpPost]
        public IHttpActionResult GetDetentionsByDate([FromBody] DateTime date)
        {
            var detentionsList = _detentionService.GetDetentionsByDate(date);
            if (detentionsList == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Не таких задержания");
                return NotFound();
            }
            return Ok(detentionsList);
        }


        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        public IHttpActionResult InsertDetention([FromBody] Detention detention)
        {
            if (!ModelState.IsValid)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
                return BadRequest(ModelState);
            }

            if (_employeeService.GetEmployeeByID(detention.DetainedByEmployeeID) == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Не такого сотрудника");
                return BadRequest("Нет сотрудника");
            }

            _detentionService.InsertDetention(detention);
            detention.DetentionID = _detentionService.LastDetention();
            return Ok(detention);
        }


        [Authorize(Roles = "Admin,Editor")]
        [HttpPut]
        public IHttpActionResult UpdateDetention(int id, [FromBody] Detention detention)
        {
            if (!ModelState.IsValid)
            {
               CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
               return BadRequest(ModelState);
            }
            if (_detentionService.GetDetentionByID(id) == null)
            {
               CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет такого задержания");
               return NotFound();
            }
            if (_employeeService.GetEmployeeByID(detention.DetainedByEmployeeID) == null)
            {
               CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет такого сотрудника");
               return BadRequest("Нет сотрудника");
            }

            _detentionService.UpdateDetention(id, detention);
            return Ok(detention);
        }


        [Authorize(Roles = "Admin,Editor")]
        [HttpDelete]
        public IHttpActionResult DeleteDetention(int id)
        {
            var detentionForDelete = _detentionService.GetDetentionByID(id);
            if (detentionForDelete == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет такого задержания");
                return NotFound();
            }
            _detentionService.DeleteDetention(id);
            return Ok(detentionForDelete);
        }
    }
}
