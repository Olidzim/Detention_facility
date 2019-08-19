using System;
using System.ComponentModel.DataAnnotations;

namespace Detention_facility.Models
{
    public class SmartRelease
    {
        public int ReleaseID { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public double AmountAccrued { get; set; }
        [Required]
        public double AmountPaid { get; set; }
        [Required]
        public string EmployeeFullName { get; set; }

    }
}