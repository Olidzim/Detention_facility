using Detention_facility.Models;
using System.Collections.Generic;
using Detention_facility.Data;

namespace Detention_facility.Business
{
    public class DetentionBusinessLayer : IDetentionBusinessLayer
    {
        private IDetentionDataAccess _detentionDataProvider;

        public DetentionBusinessLayer(IDetentionDataAccess detentionDataProvider)
        {
            _detentionDataProvider = detentionDataProvider;
        }

        public void InsertDetention(Detention detention)
        {
            _detentionDataProvider.InsertDetention(detention);
        }

        public void UpdateDetention(int id, Detention detention)
        {
            _detentionDataProvider.UpdateDetention(id, detention);
        }

        public void DeleteDetention(int id)
        {
            _detentionDataProvider.DeleteDetention(id);
        }

        public Detention GetDetentionByID(int id)
        {
            return _detentionDataProvider.GetDetentionByID(id);
        }

        public List<Detention> GetDetentions()
        {
            return _detentionDataProvider.GetDetentions();
        }
    }
}