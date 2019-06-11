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

        [Authorize(Roles ="Admin,Editor")] 
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

        [Authorize(Roles ="Admin,Editor")] 
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

        [Authorize(Roles ="Admin,Editor")] 
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

