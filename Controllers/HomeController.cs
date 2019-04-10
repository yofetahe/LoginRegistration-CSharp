using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginRegistration.Models;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Identity;

namespace LoginRegistration.Controllers
{
    public class HomeController : Controller
    {

        private LoginRegContext dbContext;

        public HomeController(LoginRegContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.userFlag = "";
            return View();
        }

        [HttpPost("LoginForm")]
        public IActionResult getLoginForm()
        {
            return View("Login");
        }

        [HttpPost("RegistrationForm")]
        public IActionResult getRegistrationForm()
        {
            return View("Index");
        }

        [HttpPost("Register")]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == user.Email)){
                    ModelState.AddModelError("Email", "This Email already exist");
                    return View("Index");
                }

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);

                dbContext.Add(user);
                dbContext.SaveChanges();

                HttpContext.Session.SetString("email", user.Email);
                
                return RedirectToAction("Success");
            } else {
                return View("Index");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(Login user)
        {
            if(ModelState.IsValid)
            {
                User userInfo = dbContext.Users.SingleOrDefault(u => u.Email == user.Email);
                if(userInfo == null)
                {
                    ModelState.AddModelError("Email", "Invalid User");
                    return View("Login");
                }

                PasswordHasher<Login> Hasher = new PasswordHasher<Login>();
                var result = Hasher.VerifyHashedPassword(user, userInfo.Password, user.Password);
                if(result == 0)
                {
                    ModelState.AddModelError("Email", "Invalid User");
                    return View("Login");
                }

                HttpContext.Session.SetString("email", user.Email);
                
                return RedirectToAction("Success");
            } else {
                return View("Login");
            }
        }

        [HttpGet("Success")]
        public IActionResult Success()
        {
            string sessionUser = HttpContext.Session.GetString("email");
            if(sessionUser is null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.userFlag = "true";

            return View("Success");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("email");
            
            return View("Index");
        }
    }
}
