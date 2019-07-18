using Detention_facility.Data;
using Detention_facility.Models;
using System.Collections.Generic;

namespace Detention_facility.Business
{
    public class ReleaseBusinessLayer : IReleaseBusinessLayer
    {
        private IReleaseDataAccess _releaseDataProvider;
        private IDetentionDataAccess _detentionDataProvider;
        private IEmployeeDataAccess _employeeDataProvider;
        private IDetaineeDataAccess _detaineeDataProvider;

        public ReleaseBusinessLayer(IReleaseDataAccess releaseDataProvider, IDetentionDataAccess detentionDataProvider, IEmployeeDataAccess employeeDataProvider, IDetaineeDataAccess detaineeDataProvider)
        {
            _releaseDataProvider = releaseDataProvider;
            _detentionDataProvider = detentionDataProvider;
            _employeeDataProvider = employeeDataProvider;
            _detaineeDataProvider = detaineeDataProvider;
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

        public string CheckValuesForRelease(int detaineeID, int detentionID, int employeeID)
        {
            string message = null;
            if (_detaineeDataProvider.GetDetaineeByID(detaineeID) == null)
            {
                message += "[Такой задержанный отсутствует в базе данных]";
            }

            if (_detentionDataProvider.GetDetentionByID(detentionID) == null)
            {
                message += "[Такое задержание отсутствует в базе данных]";
            }

            if (_employeeDataProvider.GetEmployeeByID(employeeID) == null)
            {
                message += "[Такой сотрудник отсутствует в базе данных]";
            }

            return message;
        }

        public SmartRelease GetReleaseByIDs(int detaineeID, int detentionID)
        {
            return _releaseDataProvider.GetReleaseByIDs(detaineeID, detentionID);
        }
    }
}