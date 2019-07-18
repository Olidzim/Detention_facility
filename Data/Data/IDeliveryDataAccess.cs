using Detention_facility.Models;
using System.Collections.Generic;

namespace Detention_facility.Data
{
    public interface IDeliveryDataAccess
    {
        void InsertDelivery(Delivery delivery);
        void UpdateDelivery(int id, Delivery delivery);
        void DeleteDelivery(int id);
        Delivery GetDeliveryByID(int id);
        List<Delivery> GetDeliveries();
        SmartDelivery GetDeliveryByIDs(int detaineeID, int detentionID);
    }
}