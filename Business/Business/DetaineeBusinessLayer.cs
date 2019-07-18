using Detention_facility.Models;
using System.Collections.Generic;
using Detention_facility.Data;

namespace Detention_facility.Business
{
    public class DetaineeBusinessLayer : IDetaineeBusinessLayer
    {
        private IDetaineeDataAccess _detaineeDataProvider;
        private IDetentionDataAccess _detentionDataProvider;

        public DetaineeBusinessLayer(IDetaineeDataAccess detaineeDataProvider, IDetentionDataAccess detentionDataProvider)
        {
            _detaineeDataProvider = detaineeDataProvider;
            _detentionDataProvider = detentionDataProvider;
        }

        public int InsertDetainee(Detainee detainee)
        {
           return _detaineeDataProvider.InsertDetainee(detainee);
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

        public List<SmartDetainee> GetDetaineesByDetentionID(int id)
        {
            return _detaineeDataProvider.GetDetaineesByDetentionID(id);
        }

        public void AddDetaineeToDetention(int detaineeID, int detentionID)
        {
            _detaineeDataProvider.AddDetaineeToDetention(detaineeID, detentionID);
        }

        public bool CheckDetaineeInDetention(int detaineeID, int detentionID)
        {
            return _detaineeDataProvider.CheckDetaineeInDetention(detaineeID, detentionID);
        }

        public string CheckValuesForAddDetainee(int detaineeID, int detentionID)
        {
            string message = null;
            if (_detaineeDataProvider.CheckDetaineeInDetention(detaineeID, detentionID))
                message = "[Такой задержанный уже имеется в данном задержании]";
            if (_detaineeDataProvider.GetDetaineeByID(detaineeID) == null)
                message += "[Такой задержанный отсутствует в базе данных]";
            if (_detentionDataProvider.GetDetentionByID(detentionID) == null)
                message += "[Такое задержание отсутствует в базе данных]";
            return message;
        }

        public List<SmartDetainee> Detainees(string term)
        {
            return _detaineeDataProvider.Detainees(term);
        }

        public List<SmartDetainee> GetDetaineesByAddres(string term)
        {
            return _detaineeDataProvider.GetDetaineesByAddress(term);
        }

        public int LastDetainee()
        {
            return _detaineeDataProvider.LastDetainee();
        }
    }
}