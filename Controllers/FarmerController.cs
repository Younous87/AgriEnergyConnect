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
        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        private readonly IFarmerRepository _farmerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _categoryRepository;

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Constructor with dependency injection for required repositories
        public FarmerController(
            IFarmerRepository farmerRepository,
            IProductRepository productRepository,
            IProductCategoryRepository categoryRepository)
        {
            _farmerRepository = farmerRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // GET: Farmer Dashboard - displays all products owned by the logged-in farmer
        public async Task<IActionResult> Dashboard()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var farmer = await _farmerRepository.GetByUserIdAsync(userId);

            if (farmer == null)
            {
                return NotFound(); 
            }

            // Fetch all products for the farmer
            var products = await _productRepository.GetByFarmerIdAsync(farmer.FarmerId);
            ViewBag.Farmer = farmer; 
            return View(products); 
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // GET: Add Product - displays form to add a new product
        [Authorize(Roles = "Farmer")]
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            // Load categories for dropdown list
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

            return View();
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // POST: Handle product creation logic
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(Product product)
        {
            // Get the current farmer from the user context
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var farmer = await _farmerRepository.GetByUserIdAsync(userId);

            if (farmer == null)
            {
                return NotFound();
            }

            // Assign values related to farmer and product 
            product.FarmerId = farmer.FarmerId;
            product.CreatedDate = DateTime.Now;
            product.Farmer = farmer;
            product.Category = await _categoryRepository.GetByIdAsync(product.CategoryId);

            // Save the new product
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            TempData["SuccessMessage"] = "Product added successfully!";
            return RedirectToAction(nameof(Dashboard)); 
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // GET: Show edit form for a specific product
        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            // Check if the product exists
            if (product == null)
            {
                return NotFound(); 
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var farmer = await _farmerRepository.GetByUserIdAsync(userId);

            // Only allow the product's owner to edit it
            if (product.FarmerId != farmer.FarmerId)
            {
                return Forbid(); 
            }

            // Load category list for dropdown
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

            return View(product); 
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // POST: Handle updates to an existing product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(Product product)
        {

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var farmer = await _farmerRepository.GetByUserIdAsync(userId);

            // Prevent editing products owned by others
            if (product.FarmerId != farmer.FarmerId)
            {
                return Forbid();
            }
            
            var existingProduct = await _productRepository.GetByIdAsync(product.ProductId);

            // Check if the product exists
            if (existingProduct == null)
            {
                return NotFound(); 
            }

            // Update fields with new values
            existingProduct.ProductName = product.ProductName;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.ProductionDate = product.ProductionDate;
            existingProduct.QuantityAvailable = product.QuantityAvailable;
            existingProduct.UnitOfMeasure = product.UnitOfMeasure;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            existingProduct.IsOrganic = product.IsOrganic;

            // Save updates
            await _productRepository.UpdateAsync(existingProduct);
            await _productRepository.SaveChangesAsync();

            TempData["SuccessMessage"] = "Product updated successfully!";
            return RedirectToAction(nameof(Dashboard)); // Go back to dashboard
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // GET: Show the logged-in farmer's profile
        public async Task<IActionResult> Profile()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var farmer = await _farmerRepository.GetByUserIdAsync(userId);

            // Check if the farmer exists
            if (farmer == null)
            {
                return NotFound(); 
            }

            return View(farmer); 
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
    }
}//°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°...ooo000 END OF FILE 000ooo...°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
