using Detention_facility.Models;
using System.Collections.Generic;

namespace Detention_facility.Business
{
    public interface IDetaineeBusinessLayer
    {
        int InsertDetainee(Detainee detainee);
        void UpdateDetainee(int id, Detainee detainee);
        void DeleteDetainee(int id);
        Detainee GetDetaineeByID(int id);
        List<Detainee> GetDetainees();
        List<SmartDetainee> GetDetaineesByDetentionID(int id);
        void AddDetaineeToDetention(int detaineeID, int detentionID);
        bool CheckDetaineeInDetention(int detaineeID, int detentionID);
        string CheckValuesForAddDetainee(int detaineeID, int detentionID);
        List<SmartDetainee> Detainees(string term);
        int LastDetainee();
        List<SmartDetainee> GetDetaineesByAddres(string term);
    }
}