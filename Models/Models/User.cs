using Models.Custom;
using System.ComponentModel.DataAnnotations;

namespace Detention_facility.Models
{
    public class User
    {
        public string UserId { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [ValidValues("User", "Editor", "Admin")]
        public string Role { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}

