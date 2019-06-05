using System;
using System.ComponentModel.DataAnnotations;

namespace Detention_facility.Models
{
    public class Release
    {   
        public int ReleaseID { get; set; }
        [Required]
        public int DetaineeID { get; set; }
        [Required]
        public int DetentionID { get; set; }
        [Required]
        public int PlaceID { get; set; }
        [Required]
        public int ReleasedByEmployeeID { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
    }
}