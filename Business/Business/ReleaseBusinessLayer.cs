using Detention_facility.Models;
using System.Collections.Generic;
using Detention_facility.Data;

namespace Detention_facility.Business
{
    public class ReleaseBusinessLayer : IReleaseBusinessLayer
    {
        private IReleaseDataAccess _releaseDataProvider;

        public ReleaseBusinessLayer(IReleaseDataAccess releaseDataProvider)
        {
            _releaseDataProvider = releaseDataProvider;
        }

        public void InsertRelease(Release release)
        {
            _releaseDataProvider.InsertRelease(release);
        }

        public void UpdateRelease(int id, Release release)
        {
            _releaseDataProvider.UpdateRelease(id, release);
        }

        public void DeleteRelease(int id)
        {
            _releaseDataProvider.DeleteRelease(id);
        }

        public Release GetReleaseByID(int id)
        {
            return _releaseDataProvider.GetReleaseByID(id);
        }

        public List<Release> GetReleases()
        {
            return _releaseDataProvider.GetReleases();
        }
    }
}