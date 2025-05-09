using System.ComponentModel.DataAnnotations;

namespace PROG7311_POE.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; }

        public DateTime CreatedDate { get; set; }

        // Navigation property
        public virtual Farmer Farmer { get; set; }
    }
}
