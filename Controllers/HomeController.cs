using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PROG7311_POE.Data.Repositories.Interfaces;
using PROG7311_POE.Models;

namespace PROG7311_POE.Controllers;

public class HomeController : Controller
{
    //같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같//

    private readonly IProductRepository _productRepository;

    //같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같//

    // Constructor with dependency injection for the product repository
    public HomeController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    //같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같//

    // Home page - shows the most recent products
    public async Task<IActionResult> Index()
    {
        // Retrieve the 5 most recent products to display on the homepage
        var recentProducts = await _productRepository.GetRecentProductsAsync(5);
        return View(recentProducts); 
    }

    //같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같//

    // Privacy page
    public IActionResult Privacy()
    {
        return View(); 
    }

    //같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같//

    // About page
    public IActionResult About()
    {
        return View(); 
    }

    //같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같//

    // Error page to show the most recent error
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    //같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같같//

}//같같같같같같같같같같같같같같같같같같같같...ooo000 END OF FILE 000ooo...같같같같같같같같같같같같같같같같같같같같//
