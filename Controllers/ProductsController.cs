using Microsoft.AspNetCore.Mvc;
using PROG7311_POE.Data.Repositories.Interfaces;

namespace PROG7311_POE.Controllers
{
    public class ProductsController : Controller
    {
        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _categoryRepository;

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Constructor with dependency injection for product and category repositories
        public ProductsController(
            IProductRepository productRepository,
            IProductCategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // GET: Displays a list of all products
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Fetch all products from the repository
            var products = await _productRepository.GetAllAsync();

            return View(products); 
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // GET: Displays detailed information about a specific product
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            // Fetch product details by ID
            var product = await _productRepository.GetByIdAsync(id);

            // Check if product exists
            if (product == null)
            {
                return NotFound(); 
            }

            return View(product); 
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // GET: Shows products that belong to a specific category
        [HttpGet]
        public async Task<IActionResult> ByCategory(int id)
        {

            // Fetch category details by ID
            var category = await _categoryRepository.GetByIdAsync(id);

            // Check if category exists
            if (category == null)
            {
                return NotFound(); 
            }

            // Filter products based on the category
            var products = await _productRepository.FilterProductsAsync(id, null, null, false, null);

            // Pass the category name to the view using ViewBag
            ViewBag.CategoryName = category.CategoryName;

            return View(products); 
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

    }
}//°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°...ooo000 END OF FILE 000ooo...°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
