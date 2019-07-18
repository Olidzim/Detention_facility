using System;
using System.ComponentModel.DataAnnotations;

namespace Detention_facility.Models
{
    public class SmartDelivery
    {
        public int DeliveryID { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [Required]
        public string PlaceAddress { get; set; }
        [Required]
        public string EmployeeFullName { get; set; }

    }
}