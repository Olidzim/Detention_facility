using System;
using System.ComponentModel.DataAnnotations;

namespace Detention_facility.Models
{
    public class Delivery
    {      
        public int DeliveryID { get; set; }
        [Required]
        public int DetaineeID { get; set; }
        [Required]
        public int DetentionID { get; set; }
        [Required]
        public int PlaceID { get; set; }
        [Required]
        public int DeliveredByEmployeeID { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
    }
}