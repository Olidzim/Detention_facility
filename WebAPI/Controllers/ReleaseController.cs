using Detention_facility.Business;
using Detention_facility.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Detention_facility.Controllers
{
    public class ReleaseController : ApiController
    {
        private IReleaseBusinessLayer _releaseService;

        public ReleaseController(IReleaseBusinessLayer releaseService)
        {
            _releaseService = releaseService;
        }

        [Authorize(Roles ="Admin,Editor")] 
        [HttpPost]
        public IHttpActionResult InsertRelease([FromBody] Release release)
        {
            if (ModelState.IsValid)
            {
                _releaseService.InsertRelease(release);
                return Ok(release);
            }
            if (_releaseService.CheckValuesForRelease(release.DetaineeID, release.DetentionID, release.ReleasedByEmployeeID) != null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, _releaseService.CheckValuesForRelease(release.DetaineeID, release.DetentionID, release.ReleasedByEmployeeID));
                return BadRequest(_releaseService.CheckValuesForRelease(release.DetaineeID, release.DetentionID, release.ReleasedByEmployeeID));
            }          
            CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
            return BadRequest(ModelState);
        }

        [Authorize(Roles ="Admin,Editor")] 
        [HttpPut]
        public IHttpActionResult UpdateRelease(int id, [FromBody] Release release)
        {
            if (ModelState.IsValid)
            {
                _releaseService.UpdateRelease(id, release);
                return Ok(release);
            }
            if (_releaseService.CheckValuesForRelease(release.DetaineeID, release.DetentionID, release.ReleasedByEmployeeID) != null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, _releaseService.CheckValuesForRelease(release.DetaineeID, release.DetentionID, release.ReleasedByEmployeeID));
                return BadRequest(_releaseService.CheckValuesForRelease(release.DetaineeID, release.DetentionID, release.ReleasedByEmployeeID));
            }
            CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
            return BadRequest(ModelState);

        }
       
        [HttpGet]
        public IHttpActionResult GetRelease(int id)
        {           
            var release = _releaseService.GetReleaseByID(id);
            if (release == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет освобождения с таким номером");
                return NotFound();
            }
            return Ok(release);
        }

        [Authorize(Roles ="Admin,Editor")] 
        [HttpDelete]
        public IHttpActionResult DeleteRelease(int id)
        {
            var release = _releaseService.GetReleaseByID(id);
            if (release == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет освобождения с таким номером");
                return NotFound();
            }
            _releaseService.DeleteRelease(id);
            return Ok(release);
        }

        [Authorize(Roles ="Admin,Editor")] 
        [HttpGet]
        public IHttpActionResult GetReleases()
        {            
           var releasesList = _releaseService.GetReleases();
            if (releasesList == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет освобождения с таким номером");
                return NotFound();
            }
            return Ok(releasesList);
        }
    }    
}
