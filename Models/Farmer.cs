using System.ComponentModel.DataAnnotations;

namespace PROG7311_POE.Models
{
    public class Farmer
    {
        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Primary key for the Farmer entity
        public int FarmerId { get; set; }

        // Foreign key referencing the associated User
        [Required]
        public int UserId { get; set; }

        // Name of the farm; required field with validation message and display label
        [Required(ErrorMessage = "Farm name is required")]
        [Display(Name = "Farm Name")]
        public string FarmName { get; set; }

        // Name of the farm owner; required field with validation message and display label
        [Required(ErrorMessage = "Owner name is required")]
        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }

        // Physical address of the farm
        public string Address { get; set; }

        // Phone number with display label and validation for correct phone number format
        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; }

        // Email address with validation for proper email format
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        // Date the farmer joined
        [Display(Name = "Joined Date")]
        [DataType(DataType.Date)]
        public DateTime JoinedDate { get; set; }

        // Navigation property for the related User entity 
        public virtual User User { get; set; }

        // Navigation property for the products associated with this farmer
        public virtual ICollection<Product> Products { get; set; }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
    }
}//°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°...ooo000 END OF FILE 000ooo...°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
