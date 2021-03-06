﻿using Detention_facility.Business;
using Detention_facility.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Detention_facility.Controllers
{
    public class DeliveryController : ApiController
    {
        private IDeliveryBusinessLayer _deliveryService;


        public DeliveryController(IDeliveryBusinessLayer deliveryService)
        {
            _deliveryService = deliveryService;
        }


        [Authorize(Roles ="Admin,Editor")] 
        [HttpPost]
        public IHttpActionResult InsertDelivery([FromBody] Delivery delivery)
        {
            if (!ModelState.IsValid)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
                return BadRequest(ModelState);            
            }
            if (_deliveryService.CheckValuesForDelivery(delivery.DetaineeID, delivery.DetentionID, delivery.DeliveredByEmployeeID) != null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, _deliveryService.CheckValuesForDelivery(delivery.DetaineeID, delivery.DetentionID, delivery.DeliveredByEmployeeID));
                return BadRequest(_deliveryService.CheckValuesForDelivery(delivery.DetaineeID, delivery.DetentionID, delivery.DeliveredByEmployeeID));
            }
            _deliveryService.InsertDelivery(delivery);
            return Ok(delivery);
        }


        [Authorize(Roles ="Admin,Editor")] 
        [HttpPut]
        public IHttpActionResult UpdateDelivery(int id, [FromBody] Delivery delivery)
        {
            if (!ModelState.IsValid)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
                return BadRequest(ModelState);         
            }
            if (_deliveryService.CheckValuesForDelivery(delivery.DetaineeID, delivery.DetentionID, delivery.DeliveredByEmployeeID) != null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, _deliveryService.CheckValuesForDelivery(delivery.DetaineeID, delivery.DetentionID, delivery.DeliveredByEmployeeID));
                return BadRequest(_deliveryService.CheckValuesForDelivery(delivery.DetaineeID, delivery.DetentionID, delivery.DeliveredByEmployeeID));
            }
            _deliveryService.UpdateDelivery(id, delivery);
            return Ok(delivery);
        }


        [Authorize(Roles = "Admin,Editor,User")]
        [HttpGet]
        public IHttpActionResult GetDelivery(int id)
        {
            var delivery = _deliveryService.GetDeliveryByID(id);
            if (delivery == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Не существует доставки с таким номером");
                return NotFound();
            }
            return Ok(delivery);
        }


        [Authorize(Roles = "Admin,Editor,User")]
        [Route("Api/Delivery/GetSmartDeliveryByIDs/{detaineeID}/{detentionID}")]
        [HttpGet]
        public IHttpActionResult GetSmartDeliveryByIDs(int detaineeID, int detentionID)
        {
            var delivery = _deliveryService.GetSmartDeliveryByIDs(detaineeID, detentionID);
            if (delivery == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Не существует доставки с таким номером");
                return NotFound();
            }
            return Ok(delivery);
        }


        [Authorize(Roles = "Admin,Editor,User")]
        [HttpPost]
        public IHttpActionResult GetSmartDeliveriesByDate([FromBody] DateTime date)
        {
            var deliveriesList = _deliveryService.GetSmartDeliveriesByDate(date);
            if (deliveriesList == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Нет доставок");
                return NotFound();
            }
            return Ok(deliveriesList);
        }


        [Authorize(Roles = "Admin,Editor,User")]
        [Route("Api/Delivery/GetDeliveryByIDs/{detaineeID}/{detentionID}")]
        [HttpGet]
        public IHttpActionResult GetDeliveryByIDs(int detaineeID, int detentionID)
        {
            var delivery = _deliveryService.GetDeliveryByIDs(detaineeID, detentionID);
            if (delivery == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Не существует доставки с таким номером");
                return NotFound();
            }
            return Ok(delivery);
        }
        

        [Authorize(Roles ="Admin,Editor")] 
        [HttpDelete]
        public IHttpActionResult DeleteDelivery(int id)
        {
            var delivery = _deliveryService.GetDeliveryByID(id);
            if (delivery == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Не существует доставки с таким номером");
                return NotFound();
            }
            _deliveryService.DeleteDelivery(id);
            return Ok(delivery);
        }


        [Authorize(Roles ="Admin,Editor,User")] 
        [HttpGet]
        public IHttpActionResult GetDeliveries()
        {            
            var deliveriesList = _deliveryService.GetDeliveries();
            if (deliveriesList == null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, "Не существует доставки с таким номером");
                return NotFound();
            }
            return Ok(deliveriesList);
        }
        
    }
}

