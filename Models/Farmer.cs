using System.ComponentModel.DataAnnotations;

namespace PROG7311_POE.Models
{
    public class Farmer
    {
        public int FarmerId { get; set; }

        [Required]
        public int UserId { get; set; }

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

        [Display(Name = "Joined Date")]
        [DataType(DataType.Date)]
        public DateTime JoinedDate { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
