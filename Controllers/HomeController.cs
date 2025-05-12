using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PROG7311_POE.Data.Repositories.Interfaces;
using PROG7311_POE.Models;

namespace PROG7311_POE.Controllers;

public class HomeController : Controller
{
    //��������������������������������������������������������������������������������������������������������//

    private readonly IProductRepository _productRepository;

    //��������������������������������������������������������������������������������������������������������//

    // Constructor with dependency injection for the product repository
    public HomeController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    //��������������������������������������������������������������������������������������������������������//

    // Home page - shows the most recent products
    public async Task<IActionResult> Index()
    {
        // Retrieve the 5 most recent products to display on the homepage
        var recentProducts = await _productRepository.GetRecentProductsAsync(5);
        return View(recentProducts); 
    }

    //��������������������������������������������������������������������������������������������������������//

    // Privacy page
    public IActionResult Privacy()
    {
        return View(); 
    }

    //��������������������������������������������������������������������������������������������������������//

    // About page
    public IActionResult About()
    {
        return View(); 
    }

    //��������������������������������������������������������������������������������������������������������//

    // Error page to show the most recent error
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    //��������������������������������������������������������������������������������������������������������//

}//����������������������������������������...ooo000 END OF FILE 000ooo...����������������������������������������//
