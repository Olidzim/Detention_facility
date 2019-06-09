using Detention_facility.Models;
using System.Collections.Generic;

namespace Detention_facility.Data
{
    public interface IDetaineeDataAccess
    {
        void InsertDetainee(Detainee employee);
        void UpdateDetainee(int id, Detainee employee);
        void DeleteDetainee(int id);
        Detainee GetDetaineeByID(int id);
        List<Detainee> GetDetainees();
        List<Detainee> GetDetaineesByDetentionID(int id);
        void AddDetaineeToDetention(int detaineeID, int detentionID);
        bool CheckDetaineeInDetention(int detaineeID, int detentionID);
    }
}
