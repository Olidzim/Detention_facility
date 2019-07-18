using System;
using System.ComponentModel.DataAnnotations;

namespace Detention_facility.Models
{
    public class SmartDetention
    {
        public int DetentionID { get; set; }
        [Required]
        public DateTime? DetentionDate { get; set; }
        [Required]
        public string EmployeeFullName { get; set; }

        [Required]
        public string DeliveryStatus { get; set; }

        public string ReleaseStatus { get; set; }
    }
}
