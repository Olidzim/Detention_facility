using Detention_facility.Models;
using System.Collections.Generic;
using System.Web.Http;
using Detention_facility.Business;

namespace Detention_facility.Controllers
{
    public class DetentionController : ApiController
    {
        private IDetentionBusinessLayer _detentionService;

        public DetentionController(IDetentionBusinessLayer detentionService)
        {
            _detentionService = detentionService;
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

        [HttpDelete]
        public void DeletetDetention(int id)
        {        
            _detentionService.DeleteDetention(id);
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

    }
}
