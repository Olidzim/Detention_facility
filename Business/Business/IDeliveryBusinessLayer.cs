using Detention_facility.Models;
using System.Collections.Generic;

namespace Detention_facility.Business
{
    public interface IDeliveryBusinessLayer
    {
        void InsertDelivery(Delivery delivery);
        void UpdateDelivery(int id, Delivery delivery);
        void DeleteDelivery(int id);
        Delivery GetDeliveryByID(int id);
        List<Delivery> GetDeliveries();
        string CheckValuesForDelivery(int detaineeID, int detentionID, int employeeID);
    }
}
