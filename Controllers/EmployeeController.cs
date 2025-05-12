using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROG7311_POE.Data.Repositories.Interfaces;
using PROG7311_POE.Models;

namespace PROG7311_POE.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        private readonly IFarmerRepository _farmerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _categoryRepository;

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Constructor to inject repositories for data access
        public EmployeeController(
            IFarmerRepository farmerRepository,
            IProductRepository productRepository,
            IProductCategoryRepository categoryRepository)
        {
            _farmerRepository = farmerRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // GET: Display the dashboard with a list of all farmers
        public async Task<IActionResult> Dashboard()
        {
            var farmers = await _farmerRepository.GetAllAsync(); 
            return View(farmers);
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Default entry point
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Dashboard));
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // GET: View details of a specific farmer by ID
        [HttpGet]
        public async Task<IActionResult> ViewFarmer(int id)
        {
            var farmer = await _farmerRepository.GetByIdAsync(id);
            if (farmer == null)
            {
                return NotFound();
            }

            // Get products associated with the farmer
            var products = await _productRepository.GetByFarmerIdAsync(id);
            ViewBag.Products = products;

            return View(farmer); 
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // GET: Show the Products page with filter options
        [HttpGet]
        public async Task<IActionResult> Products()
        {
            var viewModel = new ProductFilterViewModel
            {
                Categories = (await _categoryRepository.GetAllAsync()).ToList(), 
                Farmers = (await _farmerRepository.GetAllAsync()).ToList(),      
                Products = (await _productRepository.GetAllAsync()).ToList()    
            };

            return View(viewModel); 
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // POST: Handle product filtering based on the input
        [HttpPost]
        public async Task<IActionResult> Products(ProductFilterViewModel filter)
        {
            // Reload dropdown values after post
            filter.Categories = (await _categoryRepository.GetAllAsync()).ToList();
            filter.Farmers = (await _farmerRepository.GetAllAsync()).ToList();

            // Filter products based on selected criteria
            filter.Products = (await _productRepository.FilterProductsAsync(
                filter.CategoryId,
                filter.StartDate,
                filter.EndDate,
                filter.OrganicOnly,
                filter.FarmerId
            )).ToList();

            return View(filter); 
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // GET: Show details of a specific product by ID
        [HttpGet]
        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _productRepository.GetByIdAsync(id); 
            if (product == null)
            {
                return NotFound(); 
            }

            return View(product); 
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
    }
}//°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°...ooo000 END OF FILE 000ooo...°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
