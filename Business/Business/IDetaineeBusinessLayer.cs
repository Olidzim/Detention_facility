using Detention_facility.Models;
using System.Collections.Generic;

namespace Detention_facility.Business
{
    public interface IDetaineeBusinessLayer
    {
        void InsertDetainee(Detainee detainee);
        void UpdateDetainee(int id, Detainee detainee);
        void DeleteDetainee(int id);
        Detainee GetDetaineeByID(int id);
        List<Detainee> GetDetainees();
        List<Detainee> GetDetaineesByDetentionID(int id);
    }
}