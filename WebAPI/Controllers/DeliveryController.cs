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
        public IHttpActionResult InsertDelivery([FromBody] Delivery deliveries)
        {
            if (ModelState.IsValid)
            {
                _deliveryService.InsertDelivery(deliveries);
                return Ok(deliveries);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IHttpActionResult UpdateDelivery(int id, [FromBody] Delivery deliveries)
        {
            if (ModelState.IsValid)
            {
                _deliveryService.UpdateDelivery(id, deliveries);
                return Ok(deliveries);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public IHttpActionResult GetDelivery(int id)
        {
            Delivery deliveries = _deliveryService.GetDeliveryByID(id);
            if (deliveries == null)
            {
                return NotFound();
            }
            return Ok(deliveries);
        }

        [HttpDelete]
        public void DeleteDelivery(int id)
        {
            _deliveryService.DeleteDelivery(id);
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

