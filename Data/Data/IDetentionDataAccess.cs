using Detention_facility.Models;
using System.Collections.Generic;

namespace Detention_facility.Data
{
    public interface IDetentionDataAccess
    {
        void InsertDetention(Detention detention);
        void UpdateDetention(int id, Detention detention);
        void DeleteDetention(int id);
        Detention GetDetentionByID(int id);
        List<Detention> GetDetentions();
    }
}
