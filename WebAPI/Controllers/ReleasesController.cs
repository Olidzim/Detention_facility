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

        [HttpDelete]
        public void DeleteRelease(int id)
        {       
            _releaseService.DeleteRelease(id);
        }

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
