using System;
using System.ComponentModel.DataAnnotations;

namespace Detention_facility.Models
{
    public class SmartDetainee
    {
        public int DetaineeID { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string MaritalStatus { get; set; }
        [Required]
        public string Job { get; set; }
        [Required]
        public string MobilePhoneNumber { get; set; }
        [Required]
        public string HomePhoneNumber { get; set; }
        [Required]       
        public string ResidencePlace { get; set; }
    }
}
