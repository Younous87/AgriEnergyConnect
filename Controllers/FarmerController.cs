using Microsoft.AspNetCore.Mvc;

namespace PROG7311_POE.Controllers
{
    public class FarmerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
