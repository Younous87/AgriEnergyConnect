using System.ComponentModel.DataAnnotations;

namespace PROG7311_POE.Models
{
    public class RegisterFarmerViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Farm name is required")]
        [Display(Name = "Farm Name")]
        public string FarmName { get; set; }

        [Required(ErrorMessage = "Owner name is required")]
        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }

        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
    }

}
