using Detention_facility.Models;
using System.Collections.Generic;
using System.Web.Http;
using Detention_facility.Business;
using System;

namespace Detention_facility.Controllers
{
    public class DetentionController : ApiController
    {
        private IDetentionBusinessLayer _detentionService;

        public DetentionController(IDetentionBusinessLayer detentionService)
        {
            _detentionService = detentionService;
        }

        [HttpGet]
        public IHttpActionResult GetDetention(int id)
        {            
            var detention = _detentionService.GetDetentionByID(id);
            if (detention == null)
            {
                return NotFound();
            }
            return Ok(detention);
        }

        [HttpGet]
        public IHttpActionResult GetDetentions()
        {
            List<Detention> detentions_list = _detentionService.GetDetentions();
            if (detentions_list == null)
            {
                return NotFound();
            }
            return Ok(detentions_list);
        }
    
        [HttpGet]
        public IHttpActionResult GetDetentionsByPlace([FromBody] string place)
        {
            List<Detention> detentions_list = _detentionService.GetDetentionsByPlace(place);
            if (detentions_list == null)
            {
                return NotFound();
            }
            return Ok(detentions_list);
        }
     
        [HttpGet]
        public IHttpActionResult GetDetentionsByLastName([FromBody] string lastname)
        {
            List<Detention> detentions_list = _detentionService.GetDetentionsByLastName(lastname);
            if (detentions_list == null)
            {
                return NotFound();
            }
            return Ok(detentions_list);
        }

        //[Route("Api/Detention/GetDetentionsByDate/{date:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [HttpGet]
        public IHttpActionResult GetDetentionsByDate([FromBody] DateTime date)
        {            
            List<Detention> detentions_list = _detentionService.GetDetentionsByDate(date);
            if (detentions_list == null)
            {
                return NotFound();
            }
            return Ok(detentions_list);
        }
       
        [HttpPost]
        public IHttpActionResult InsertDetention([FromBody] Detention detention)
        {
            if (ModelState.IsValid)
            {
                _detentionService.InsertDetention(detention);
                return Ok(detention);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IHttpActionResult UpdateDetention(int id, [FromBody] Detention detention)
        {
            if (ModelState.IsValid)
            {
                _detentionService.UpdateDetention(id, detention);
                return Ok(detention);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public void DeletetDetention(int id)
        {
            _detentionService.DeleteDetention(id);
        }

    }
}
