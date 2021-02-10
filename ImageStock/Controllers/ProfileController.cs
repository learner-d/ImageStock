using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ImageStock.Data;
using ImageStock.Data.Models;
using ImageStock.Data.Models.Views;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImageStock.Controllers
{
    public class ProfileController : Controller
    {
        private AppDbContext _appDbContext;
        public ProfileController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Brief()
        {
            return PartialView(_appDbContext.Users.Where(usr => usr.Username == User.Identity.Name).FirstOrDefault());
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginFormData data)
        {
            if (ModelState.IsValid)
            {
                UserProfile userProfile = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == data.Username);
                if (userProfile != null)
                {
                    if (userProfile.PwdHash == data.Password)
                    {
                        await Authenticate(data.Username);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError("", "Не правильний пароль");
                }
                else
                    ModelState.AddModelError("", "Не дійсне ім'я користувача");
            }

            return View(data);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Profile");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormData data)
        {
            if (ModelState.IsValid)
            {
                UserProfile userProfile = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == data.Username);
                if (userProfile == null)
                {
                    userProfile = new UserProfile {
                        Username = data.Username,
                        PwdHash = data.Password
                    };
                    try
                    {
                        await _appDbContext.Users.AddAsync(userProfile);
                        await _appDbContext.SaveChangesAsync();
                        return View("RegisterSuccess", userProfile);
                    }
                    catch (Exception)
                    {
                        return View("RegisterError");
                    }
                }
                else
                    ModelState.AddModelError("", $"Користувач з іменем '{data.Username}' вже зареєстрований!");
            }

            return View(data);
        }
    }
}
