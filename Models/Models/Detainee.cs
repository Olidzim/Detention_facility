using System;
using System.ComponentModel.DataAnnotations;

namespace Detention_facility.Models
{
    public class Detainee
    {     
        public int DetaineeID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Patronymic { get; set; }
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
       // [Required]
        public string Photo { get; set; }
       // [Required]
        public string ExtraInfo { get; set; }
        [Required]
        public string ResidencePlace { get; set; }
    }
}
