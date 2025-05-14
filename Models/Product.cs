using System.ComponentModel.DataAnnotations;

namespace PROG7311_POE.Models
{
    public class Product
    {
        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Unique identifier for the product
        public int ProductId { get; set; }

        // Foreign key to the Farmer who owns this product
        [Required]
        public int FarmerId { get; set; }

        // Product name is required with a custom error message
        [Required(ErrorMessage = "Product name is required")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        // Foreign key to the Product Category
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        // Date when the product was produced
        [Required(ErrorMessage = "Production date is required")]
        [Display(Name = "Production Date")]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        // Quantity must be provided and must be greater than zero
        [Required(ErrorMessage = "Quantity is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        [Display(Name = "Quantity Available")]
        public decimal QuantityAvailable { get; set; }

        // Unit of measure is required
        [Required(ErrorMessage = "Unit of measure is required")]
        [Display(Name = "Unit of Measure")]
        public string UnitOfMeasure { get; set; }

        // Price field must be a non-negative value if provided
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        // Description field with multiline input 
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        // Indicates whether the product is organic
        [Display(Name = "Organic")]
        public bool IsOrganic { get; set; }

        // Timestamp for when the product record was created
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        // Navigation property linking to the Farmer entity
        public virtual Farmer Farmer { get; set; }

        // Navigation property linking to the ProductCategory entity
        public virtual ProductCategory Category { get; set; }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
    }
}//°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°...ooo000 END OF FILE 000ooo...°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
