using Microsoft.AspNetCore.Mvc;
using PROG7311_POE.Data.Repositories.Interfaces;

namespace PROG7311_POE.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _categoryRepository;

        public ProductsController(
            IProductRepository productRepository,
            IProductCategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> ByCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var products = await _productRepository.FilterProductsAsync(id, null, null, false, null);
            ViewBag.CategoryName = category.CategoryName;

            return View(products);
        }
    }
}
