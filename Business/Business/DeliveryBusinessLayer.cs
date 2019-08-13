using Detention_facility.Data;
using Detention_facility.Models;
using System.Collections.Generic;
using Detention_facility.Custom;
using System;

namespace Detention_facility.Business
{
    public class DeliveryBusinessLayer : IDeliveryBusinessLayer
    {
        private IDeliveryDataAccess _deliveryDataProvider;
        private IDetentionDataAccess _detentionDataProvider;
        private IEmployeeDataAccess _employeeDataProvider;
        private IDetaineeDataAccess _detaineeDataProvider;

        public DeliveryBusinessLayer(
            IDeliveryDataAccess deliveryDataProvider,
            IDetentionDataAccess detentionDataProvider,
            IEmployeeDataAccess employeeDataProvider, IDetaineeDataAccess detaineeDataProvider

            )
        {
            _deliveryDataProvider = deliveryDataProvider;
            _detentionDataProvider = detentionDataProvider;
            _employeeDataProvider = employeeDataProvider;
            _detaineeDataProvider = detaineeDataProvider;

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

        /* public List<Delivery> GetDeliveries()
         {
             return _deliveryDataProvider.GetDeliveries();
         }*/

        public ResponseClass<List<Delivery>> GetDeliveries()
        {
            return new ResponseClass<List<Delivery>>
            {
                IsSuccess = true,
                Message = "Deliveries list",
                ResponseData = _deliveryDataProvider.GetDeliveries()
            };
        }

      /* public ResponseClass<List<Delivery>> GetDeliveries()
        {
            return _deliveryResponseList.CreateResponse("Delivery list", true, _deliveryDataProvider.GetDeliveries());
        }*/

        public string CheckValuesForDelivery(int detaineeID, int detentionID, int employeeID)
        {
            string message = null;
            if (_detaineeDataProvider.GetDetaineeByID(detaineeID) == null)
            {
                message += "[Такой задержанный отсутствует в базе данных]";
            }

            if (_detentionDataProvider.GetDetentionByID(detentionID) == null)
            {
                message += "[Такое задержание отсутствует в базе данных]";
            }

            if (_employeeDataProvider.GetEmployeeByID(employeeID) == null)
            {
                message += "[Такой сотрудник отсутствует в базе данных]";
            }

            return message;
        }

        public SmartDelivery GetSmartDeliveryByIDs(int detaineeID, int detentionID)
        {
            return _deliveryDataProvider.GetSmartDeliveryByIDs(detaineeID, detentionID);
        }

        public Delivery GetDeliveryByIDs(int detaineeID, int detentionID)
        {
            return _deliveryDataProvider.GetDeliveryByIDs(detaineeID, detentionID);
        }

        public List<SmartDelivery> GetSmartDeliveriesByDate(DateTime date)
        {
            return _deliveryDataProvider.GetSmartDeliveriesByDate(date);
        }
    }
}