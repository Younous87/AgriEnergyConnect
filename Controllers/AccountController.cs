using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROG7311_POE.Data.Repositories.Interfaces;
using PROG7311_POE.Models;
using PROG7311_POE.Services.Interfaces;

namespace PROG7311_POE.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IFarmerRepository _farmerRepository;

        public AccountController(IAuthService authService, IFarmerRepository farmerRepository)
        {
            _authService = authService;
            _farmerRepository = farmerRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isValid = await _authService.ValidateUserAsync(model.Username, model.Password);
            if (!isValid)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }

            var user = await _authService.GetUserByUsernameAsync(model.Username);
            await _authService.SignInUserAsync(HttpContext, user, model.RememberMe);

            if (user.Role == "Farmer")
            {
                return RedirectToAction("Dashboard", "Farmer");
            }
            else
            {
                return RedirectToAction("Dashboard", "Employee");
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _authService.SignOutUserAsync(HttpContext).Wait();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public IActionResult RegisterFarmer()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterFarmer(RegisterFarmerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _authService.RegisterUserAsync(model.Username, model.Password, "Farmer");
            if (user == null)
            {
                ModelState.AddModelError("Username", "Username already exists");
                return View(model);
            }

            var farmer = new Farmer
            {
                UserId = user.UserId,
                FarmName = model.FarmName,
                OwnerName = model.OwnerName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                JoinedDate = DateTime.Now
            };

            await _farmerRepository.AddAsync(farmer);
            await _farmerRepository.SaveChangesAsync();

            TempData["SuccessMessage"] = "Farmer registered successfully!";
            return RedirectToAction("Index", "Employee");
        }
    }
}
