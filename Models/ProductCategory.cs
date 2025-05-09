using System.ComponentModel.DataAnnotations;

namespace PROG7311_POE.Models
{
    public class ProductCategory
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        // Navigation property
        public virtual ICollection<Product> Products { get; set; }
    }
}
