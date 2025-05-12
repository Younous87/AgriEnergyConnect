using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROG7311_POE.Data.Repositories.Interfaces;
using PROG7311_POE.Models;
using PROG7311_POE.Services.Interfaces;

namespace PROG7311_POE.Controllers
{
    public class AccountController : Controller
    {
        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        private readonly IAuthService _authService;
        private readonly IFarmerRepository _farmerRepository;

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Constructor for injecting services into the controller
        public AccountController(IAuthService authService, IFarmerRepository farmerRepository)
        {
            _authService = authService;
            _farmerRepository = farmerRepository;
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // GET: Login page
        [HttpGet]
        public IActionResult Login()
        {
            // If the user is already authenticated, redirect to the Home page
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // POST: Handle login form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // If model validation fails, re-display the login form
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Validate the user's credentials
            var isValid = await _authService.ValidateUserAsync(model.Username, model.Password);
            if (!isValid)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }

            // Retrieve the user and sign them in
            var user = await _authService.GetUserByUsernameAsync(model.Username);
            await _authService.SignInUserAsync(HttpContext, user, model.RememberMe);

            // Redirect the user based on their role
            if (user.Role == "Farmer")
            {
                return RedirectToAction("Dashboard", "Farmer");
            }
            else
            {
                return RedirectToAction("Dashboard", "Employee");
            }
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // GET: Logout and sign out the current user
        [HttpGet]
        public IActionResult Logout()
        {
            _authService.SignOutUserAsync(HttpContext).Wait();
            return RedirectToAction("Index", "Home");
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // GET: Show the form to register a new farmer (only accessible by Employees)
        [HttpGet]
        [Authorize(Roles = "Employee")]
        public IActionResult RegisterFarmer()
        {
            return View();
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // POST: Handle the form submission for registering a new farmer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterFarmer(RegisterFarmerViewModel model)
        {
            // If the input model is invalid, return the view with validation messages
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Register the user with the role of "Farmer"
            var user = await _authService.RegisterUserAsync(model.Username, model.Password, "Farmer");

            // Re-check ModelState after registration
            if (!ModelState.IsValid)
            {
                // Output any errors to the console 
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"{error.Key}: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }
                return View(model);
            }

            // Create a new Farmer object and populate it with the model data
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

            // Save the new farmer to the database
            await _farmerRepository.AddAsync(farmer);
            await _farmerRepository.SaveChangesAsync();

            // Show a success message and redirect to the employee dashboard
            TempData["SuccessMessage"] = "Farmer registered successfully!";
            return RedirectToAction("Dashboard", "Employee");
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
    }
}//°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°...ooo000 END OF FILE 000ooo...°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
