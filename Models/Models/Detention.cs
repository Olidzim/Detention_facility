using System;
using System.ComponentModel.DataAnnotations;

namespace Detention_facility.Models
{
    public class Detention
    {      
        public int DetentionID { get; set; }
        [Required]
        public DateTime? DetentionDate { get; set; }
        [Required]
        public int DetainedByEmployeeID { get; set; }
    }
}
