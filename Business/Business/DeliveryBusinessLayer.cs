using Detention_facility.Data;
using Detention_facility.Models;
using System.Collections.Generic;

namespace Detention_facility.Business
{
    public class DeliveryBusinessLayer : IDeliveryBusinessLayer
    {
        private IDeliveryDataAccess _deliveryDataProvider;

        public DeliveryBusinessLayer(IDeliveryDataAccess deliveryDataProvider)
        {
            _deliveryDataProvider = deliveryDataProvider;
        }

        public void InsertDelivery(Delivery delivery)
        {
            _deliveryDataProvider.InsertDelivery(delivery);
        }

        public void UpdateDelivery(int id, Delivery delivery)
        {
            _deliveryDataProvider.UpdateDelivery(id, delivery);
        }
        public void DeleteDelivery(int id)
        {
            _deliveryDataProvider.DeleteDelivery(id);
        }

        public Delivery GetDeliveryByID(int id)
        {
            return _deliveryDataProvider.GetDeliveryByID(id);
        }

        public List<Delivery> GetDeliveries()
        {
            return _deliveryDataProvider.GetDeliveries();
        }
    }
}