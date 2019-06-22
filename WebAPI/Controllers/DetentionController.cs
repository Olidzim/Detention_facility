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

        [Authorize(Roles = "Admin,Editor")]
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

        [HttpGet]
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

        //[Route("Api/Detention/GetDetentionsByDate/{date:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [HttpGet]
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
            if (ModelState.IsValid)
            {
                _detentionService.InsertDetention(detention);
                return Ok(detention);
            }
            if (_employeeService.GetEmployeeByID(detention.DetainedByEmployeeID) == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Не такого сотрудника");
                return BadRequest("Нет сотрудника");
            }            
            CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpPut]
        public IHttpActionResult UpdateDetention(int id, [FromBody] Detention detention)
        {
            if (ModelState.IsValid)
            {
                _detentionService.UpdateDetention(id, detention);
                return Ok(detention);
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
            CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
            return BadRequest(ModelState);
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
