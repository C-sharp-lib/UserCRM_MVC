﻿using UserCRM.DTO;
using UserCRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


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
        public IActionResult LoginUser(LoginDTO login)
        {
            var user = _context.Users.Where(u => u.Email == login.Email).SingleOrDefault();
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
        public IActionResult RegisterUser(RegisterDTO newuser)
        {
            Users CheckEmail = _context.Users
                .Where(u => u.Email == newuser.Email)
                .SingleOrDefault();

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
                    Password = Hasher.HashPassword(newuser, newuser.Password)
                };
                _context.Add(newUser);
                _context.SaveChanges();
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