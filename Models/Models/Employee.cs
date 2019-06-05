using System;
using System.ComponentModel.DataAnnotations;

namespace Detention_facility.Models
{
    public class Employee
    {      
        public int EmployeeID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Patronymic { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string EmployeeRank { get; set; }
    }
}
