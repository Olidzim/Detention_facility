using Detention_facility.Models;
using System.Collections.Generic;

namespace Detention_facility.Business
{
    public interface IReleaseBusinessLayer
    {
        void InsertRelease(Release release);
        void UpdateRelease(int id, Release release);
        void DeleteRelease(int id);
        Release GetReleaseByID(int id);
        List<Release> GetReleases();
        string CheckValuesForRelease(int detaineeID, int detentionID, int employeeID);
    }
}
