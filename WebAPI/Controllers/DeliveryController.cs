using Detention_facility.Business;
using Detention_facility.Models;
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
            if (ModelState.IsValid)
            {
                _deliveryService.InsertDelivery(delivery);
                return Ok(delivery);
            }
            if (_deliveryService.CheckValuesForDelivery(delivery.DetaineeID, delivery.DetentionID, delivery.DeliveredByEmployeeID) != null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, _deliveryService.CheckValuesForDelivery(delivery.DetaineeID, delivery.DetentionID, delivery.DeliveredByEmployeeID));
                return BadRequest(_deliveryService.CheckValuesForDelivery(delivery.DetaineeID, delivery.DetentionID, delivery.DeliveredByEmployeeID));
            }
            CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
            return BadRequest(ModelState);
        }

        [Authorize(Roles ="Admin,Editor")] 
        [HttpPut]
        public IHttpActionResult UpdateDelivery(int id, [FromBody] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                _deliveryService.UpdateDelivery(id, delivery);
                return Ok(delivery);
            }
            if (_deliveryService.CheckValuesForDelivery(delivery.DetaineeID, delivery.DetentionID, delivery.DeliveredByEmployeeID) != null)
            {
                CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, _deliveryService.CheckValuesForDelivery(delivery.DetaineeID, delivery.DetentionID, delivery.DeliveredByEmployeeID));
                return BadRequest(_deliveryService.CheckValuesForDelivery(delivery.DetaineeID, delivery.DetentionID, delivery.DeliveredByEmployeeID));
            }
            CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, CustomLogging.ModelStatusConverter(ModelState));
            return BadRequest(ModelState);
        }
       
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

        [Authorize(Roles ="Admin,Editor")] 
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

