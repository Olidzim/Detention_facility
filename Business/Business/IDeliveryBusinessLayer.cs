using Detention_facility.Models;
using System.Collections.Generic;
using Detention_facility.Custom;
using System;

namespace Detention_facility.Business
{
    public interface IDeliveryBusinessLayer
    {
        void InsertDelivery(Delivery delivery);
        void UpdateDelivery(int id, Delivery delivery);
        void DeleteDelivery(int id);
        Delivery GetDeliveryByID(int id);
        //List<Delivery> GetDeliveries();
        ResponseClass<List<Delivery>> GetDeliveries();        
        string CheckValuesForDelivery(int detaineeID, int detentionID, int employeeID);
        SmartDelivery GetSmartDeliveryByIDs(int detaineeID, int detentionID);
        Delivery GetDeliveryByIDs(int detaineeID, int detentionID);
        List<SmartDelivery> GetSmartDeliveriesByDate(DateTime date);
    }
}
