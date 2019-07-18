using System;
using System.ComponentModel.DataAnnotations;

namespace Detention_facility.Models
{
    public class SmartEmployee
    {
        public int EmployeeID { get; set; }
        [Required]
        public string FullName { get; set; }

        public string Patronymic { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string EmployeeRank { get; set; }
    }
}
