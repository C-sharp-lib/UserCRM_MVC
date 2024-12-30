using UserCRM.DTO;
using UserCRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace UserCRM.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context) 
        {
            _context = context;
        }
        private Users? ActiveUser
        {
            get
            {
                return _context.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("Id"));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterPage()
        {
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult LoginPage()
        {
            ViewBag.user = ActiveUser;
            return View();
        }
         [HttpPost("LoginUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser(LoginDTO login)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == login.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(nameof(LoginPage));
            }
            var passwordHasher = new PasswordHasher<Users>();
            var result = passwordHasher.VerifyHashedPassword(user, user.Password, login.Password);
            if (result == PasswordVerificationResult.Success)
            {
                HttpContext.Session.SetInt32("Id", user.Id);
                HttpContext.Session.SetString("UserName", user.UserName);
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View(nameof(LoginPage));
            }
        }
        [HttpPost("RegisterUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterDTO newuser)
        {
            Users CheckEmail = await _context.Users
                .SingleOrDefaultAsync(u => u.Email == newuser.Email);

            if (CheckEmail != null)
            {
                ViewBag.errors = "That email already exists";
                return RedirectToAction("RegisterPage", "Account");
            }
            if (ModelState.IsValid)
            {
                PasswordHasher<RegisterDTO> Hasher = new PasswordHasher<RegisterDTO>();
                Users newUser = new Users
                {
                    FirstName = newuser.FirstName,
                    MiddleName = newuser.MiddleName,
                    LastName = newuser.LastName,
                    Email = newuser.Email,
                    UserName = newuser.UserName,
                    Password = Hasher.HashPassword(newuser, newuser.Password),
                    CreatedAt = DateTime.Now
                };
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("LoginPage", "Account");
            }
            else
            {
                return View(nameof(RegisterPage));
            }
        }
        
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage", "Account");
        }
    }
}
