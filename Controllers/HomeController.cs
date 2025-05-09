using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PROG7311_POE.Data.Repositories.Interfaces;
using PROG7311_POE.Models;

namespace PROG7311_POE.Controllers;

public class HomeController : Controller
{
    private readonly IProductRepository _productRepository;

    public HomeController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IActionResult> Index()
    {
        // Show recent products on the home page
        var recentProducts = await _productRepository.GetRecentProductsAsync(5);
        return View(recentProducts);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
