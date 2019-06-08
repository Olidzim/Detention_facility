using Detention_facility.Models;
using System.Collections.Generic;
using Detention_facility.Data;
using System;

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

        public List<Detention> GetDetentionsByPlace(string place)
        {
            return _detentionDataProvider.GetDetentionsByPlace(place);
        }

        public List<Detention> GetDetentionsByLastName(string lastname)
        {
            return _detentionDataProvider.GetDetentionsByLastName(lastname);
        }

        public List<Detention> GetDetentionsByDate(DateTime date)
        {
            return _detentionDataProvider.GetDetentionsByDate(date);
        }    
    }
}