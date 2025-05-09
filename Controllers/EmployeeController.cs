using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROG7311_POE.Data.Repositories.Interfaces;
using PROG7311_POE.Models;

namespace PROG7311_POE.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly IFarmerRepository _farmerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _categoryRepository;

        public EmployeeController(
            IFarmerRepository farmerRepository,
            IProductRepository productRepository,
            IProductCategoryRepository categoryRepository)
        {
            _farmerRepository = farmerRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Dashboard()
        {
            var farmers = await _farmerRepository.GetAllAsync();
            return View(farmers);
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpGet]
        public async Task<IActionResult> ViewFarmer(int id)
        {
            var farmer = await _farmerRepository.GetByIdAsync(id);
            if (farmer == null)
            {
                return NotFound();
            }

            var products = await _productRepository.GetByFarmerIdAsync(id);
            ViewBag.Products = products;

            return View(farmer);
        }

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

        [HttpPost]
        public async Task<IActionResult> Products(ProductFilterViewModel filter)
        {
            filter.Categories = (await _categoryRepository.GetAllAsync()).ToList();
            filter.Farmers = (await _farmerRepository.GetAllAsync()).ToList();

            filter.Products = (await _productRepository.FilterProductsAsync(
                filter.CategoryId,
                filter.StartDate,
                filter.EndDate,
                filter.OrganicOnly,
                filter.FarmerId
            )).ToList();

            return View(filter);
        }

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
    }
}
