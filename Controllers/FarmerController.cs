using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROG7311_POE.Data.Repositories.Interfaces;
using PROG7311_POE.Models;
using System.Security.Claims;

namespace PROG7311_POE.Controllers
{
    
    public class FarmerController : Controller
    {
        private readonly IFarmerRepository _farmerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _categoryRepository;

        public FarmerController(
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
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var farmer = await _farmerRepository.GetByUserIdAsync(userId);

            if (farmer == null)
            {
                return NotFound();
            }

            var products = await _productRepository.GetByFarmerIdAsync(farmer.FarmerId);
            ViewBag.Farmer = farmer;
            return View(products);
        }

        [Authorize(Roles = "Farmer")]
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(Product product)
        {
            //if (!ModelState.IsValid)
            //{
            //    var categories = await _categoryRepository.GetAllAsync();
            //    ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            //    return View(product);
            //}

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var farmer = await _farmerRepository.GetByUserIdAsync(userId);

            if (farmer == null)
            {
                return NotFound();
            }

            product.FarmerId = farmer.FarmerId;
            product.CreatedDate = DateTime.Now;
            product.Farmer = farmer;
            product.Category = await _categoryRepository.GetByIdAsync(product.CategoryId);


            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            TempData["SuccessMessage"] = "Product added successfully!";
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var farmer = await _farmerRepository.GetByUserIdAsync(userId);

            // Security check - ensure farmer only edits their own products
            if (product.FarmerId != farmer.FarmerId)
            {
                return Forbid();
            }

            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(Product product)
        {
            //if (!ModelState.IsValid)
            //{
            //    var categories = await _categoryRepository.GetAllAsync();
            //    ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            //    return View(product);
            //}

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var farmer = await _farmerRepository.GetByUserIdAsync(userId);

            // Security check - ensure farmer only edits their own products
            if (product.FarmerId != farmer.FarmerId)
            {
                return Forbid();
            }

            var existingProduct = await _productRepository.GetByIdAsync(product.ProductId);
            if (existingProduct == null)
            {
                return NotFound();
            }

            // Update only allowed fields
            existingProduct.ProductName = product.ProductName;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.ProductionDate = product.ProductionDate;
            existingProduct.QuantityAvailable = product.QuantityAvailable;
            existingProduct.UnitOfMeasure = product.UnitOfMeasure;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            existingProduct.IsOrganic = product.IsOrganic;

            await _productRepository.UpdateAsync(existingProduct);
            await _productRepository.SaveChangesAsync();

            TempData["SuccessMessage"] = "Product updated successfully!";
            return RedirectToAction(nameof(Dashboard));
        }

        public async Task<IActionResult> Profile()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var farmer = await _farmerRepository.GetByUserIdAsync(userId);

            if (farmer == null)
            {
                return NotFound();
            }

            return View(farmer);
        }


    }
}
