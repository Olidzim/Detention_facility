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
        public IHttpActionResult Post([FromBody] Release release)
        {
            if (_releaseService.CheckValuesForRelease(release.DetaineeID, release.DetentionID, release.ReleasedByEmployeeID) != null)
            {
                return BadRequest(_releaseService.CheckValuesForRelease(release.DetaineeID, release.DetentionID, release.ReleasedByEmployeeID));
            }
            if (ModelState.IsValid)
            {                
                _releaseService.InsertRelease(release);
                return Ok(release);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles ="Admin,Editor")] 
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Release release)
        {
            if (_releaseService.CheckValuesForRelease(release.DetaineeID, release.DetentionID, release.ReleasedByEmployeeID) != null)
            {
                return BadRequest(_releaseService.CheckValuesForRelease(release.DetaineeID, release.DetentionID, release.ReleasedByEmployeeID));
            }
            if (ModelState.IsValid)
            {                
                _releaseService.UpdateRelease(id, release);
                return Ok(release);
            }
            return BadRequest(ModelState);

        }
       
        [HttpGet]
        public IHttpActionResult GetRelease(int id)
        {           
            Release release = _releaseService.GetReleaseByID(id);
            if (release == null)
            {
                return NotFound();
            }
            return Ok(release);
        }

        [Authorize(Roles ="Admin,Editor")] 
        [HttpDelete]
        public void DeleteRelease(int id)
        {       
            _releaseService.DeleteRelease(id);
        }

        [Authorize(Roles ="Admin,Editor")] 
        [HttpGet]
        public IHttpActionResult GetReleases()
        {            
            List<Release> releases_list = _releaseService.GetReleases();
            if (releases_list == null)
            {
                return NotFound();
            }
            return Ok(releases_list);
        }
    }    
}
