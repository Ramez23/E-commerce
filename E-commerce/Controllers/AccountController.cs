using Microsoft.AspNetCore.Mvc;
using E_commerce.Data;
using E_commerce.Models;

namespace E_commerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountController(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Signin()
        {
            return View(new User());
        }

        [HttpPost]
        public IActionResult Signin(User user)
        {
            if (ModelState.IsValid)
            {
                if (_dbContext.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "This email address is already registered.");
                }
                else
                {
                    var passwordValidationMessage = CheckPassword(user.Password);

                    if (!string.IsNullOrEmpty(passwordValidationMessage))
                    {
                        ModelState.AddModelError("Password", passwordValidationMessage);
                    }
                    else if (user.Password != user.ConfirmPassword)
                    {
                        ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                    }
                    else
                    {
                        _dbContext.Users.Add(user);
                        _dbContext.SaveChanges();
                        return RedirectToAction("Login", "Account");
                    }
                }
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Email == Email);

                if (user != null && user.Password == Password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "Invalid email or password");
                }
            }

            return View();
        }

        private string CheckPassword(string password)
        {
            if (password.Length < 8)
            {
                return "Password must be at least 8 characters long.";
            }

            if (!password.Any(char.IsUpper))
            {
                return "Password must contain at least one uppercase letter.";
            }

            if (!password.Any(char.IsLower))
            {
                return "Password must contain at least one lowercase letter.";
            }

            if (!password.Any(char.IsDigit))
            {
                return "Password must contain at least one number.";
            }

            return null; // Password is valid
        }
    }
}
