using System.ComponentModel.DataAnnotations;

namespace PROG7311_POE.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public int FarmerId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Production date is required")]
        [Display(Name = "Production Date")]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        [Display(Name = "Quantity Available")]
        public decimal QuantityAvailable { get; set; }

        [Required(ErrorMessage = "Unit of measure is required")]
        [Display(Name = "Unit of Measure")]
        public string UnitOfMeasure { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Organic")]
        public bool IsOrganic { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public virtual Farmer Farmer { get; set; }
        public virtual ProductCategory Category { get; set; }
    }
}
