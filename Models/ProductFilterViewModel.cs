using System.ComponentModel.DataAnnotations;

namespace PROG7311_POE.Models
{
    public class ProductFilterViewModel
    {
        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Selected category ID for filtering products 
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        // Start date for filtering products by production date range 
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        // End date for filtering products by production date range 
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        // Indicates whether to show only organic products
        [Display(Name = "Organic Only")]
        public bool OrganicOnly { get; set; }

        // Selected farmer ID for filtering products by farmer 
        [Display(Name = "Farmer")]
        public int? FarmerId { get; set; }

        // List of all product categories (used to populate dropdowns in the view)
        public List<ProductCategory> Categories { get; set; }

        // List of all farmers (used to populate dropdowns in the view)
        public List<Farmer> Farmers { get; set; }

        // List of products resulting from the filter operation
        public List<Product> Products { get; set; }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
    }
}//°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°...ooo000 END OF FILE 000ooo...°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
