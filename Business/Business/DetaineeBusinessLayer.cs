using Detention_facility.Models;
using System.Collections.Generic;
using Detention_facility.Data;

namespace Detention_facility.Business
{
    public class DetaineeBusinessLayer : IDetaineeBusinessLayer
    {
        private IDetaineeDataAccess _detaineeDataProvider;

        public DetaineeBusinessLayer(IDetaineeDataAccess detaineeDataProvider)
        {
            _detaineeDataProvider = detaineeDataProvider;
        }

        public void InsertDetainee(Detainee detainee)
        {
            _detaineeDataProvider.InsertDetainee(detainee);
        }

        public void UpdateDetainee(int id, Detainee detainee)
        {
            _detaineeDataProvider.UpdateDetainee(id, detainee);
        }

        public void DeleteDetainee(int id)
        {
            _detaineeDataProvider.DeleteDetainee(id);
        }

        public Detainee GetDetaineeByID(int id)
        {
            return _detaineeDataProvider.GetDetaineeByID(id);
        }

        public List<Detainee> GetDetainees()
        {
            return _detaineeDataProvider.GetDetainees();
        }

        public List<Detainee> GetDetaineesByDetentionID(int id)
        {
            return _detaineeDataProvider.GetDetaineesByDetentionID(id);
        }
    }
}