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
        public string PlaceAddress { get; set; }
        [Range(1, int.MaxValue)]
        public int DeliveredByEmployeeID { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
    }
}