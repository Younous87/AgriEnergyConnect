using System.ComponentModel.DataAnnotations;

namespace PROG7311_POE.Models
{
    public class ProductFilterViewModel
    {
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Organic Only")]
        public bool OrganicOnly { get; set; }

        [Display(Name = "Farmer")]
        public int? FarmerId { get; set; }

        public List<ProductCategory> Categories { get; set; }
        public List<Farmer> Farmers { get; set; }
        public List<Product> Products { get; set; }
    }
}
