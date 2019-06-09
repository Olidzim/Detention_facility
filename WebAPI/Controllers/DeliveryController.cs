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

        [HttpPost]
        public IHttpActionResult InsertDelivery([FromBody] Delivery delivery)
        {
            if (_deliveryService.CheckValuesForDelivery(delivery.DetaineeID, delivery.DetentionID, delivery.DeliveredByEmployeeID) != null)
            {
                return BadRequest(_deliveryService.CheckValuesForDelivery(delivery.DetaineeID, delivery.DetentionID, delivery.DeliveredByEmployeeID));
            }
            if (ModelState.IsValid)
            {
                _deliveryService.InsertDelivery(delivery);
                return Ok(delivery);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IHttpActionResult UpdateDelivery(int id, [FromBody] Delivery delivery)
        {
            if (_deliveryService.CheckValuesForDelivery(delivery.DetaineeID, delivery.DetentionID, delivery.DeliveredByEmployeeID) != null)
            {
                return BadRequest(_deliveryService.CheckValuesForDelivery(delivery.DetaineeID, delivery.DetentionID, delivery.DeliveredByEmployeeID));
            }
            if (ModelState.IsValid)
            {
                _deliveryService.UpdateDelivery(id, delivery);
                return Ok(delivery);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public IHttpActionResult GetDelivery(int id)
        {
            Delivery delivery = _deliveryService.GetDeliveryByID(id);
            if (delivery == null)
            {
                return NotFound();
            }
            return Ok(delivery);
        }

        [HttpDelete]
        public IHttpActionResult DeleteDelivery(int id)
        {
            Delivery delivery = _deliveryService.GetDeliveryByID(id);
            if (delivery == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetDeliveries()
        {
            List<Delivery> deliveries_list = _deliveryService.GetDeliveries();
            if (deliveries_list == null)
            {
                return NotFound();
            }
            return Ok(deliveries_list);
        }

    }
}

