using Detention_facility.Models;
using System.Collections.Generic;

namespace Detention_facility.Data
{
    public interface IReleaseDataAccess
    {
        void InsertRelease(Release release);
        void UpdateRelease(int id, Release release);
        void DeleteRelease(int id);
        Release GetReleaseByID(int id);
        List<Release> GetReleases();
        Release GetReleaseByIDs(int detaineeID, int detentionID);
    }
}