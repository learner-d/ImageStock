using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ImageStock.Data;
using ImageStock.Data.Models;
using ImageStock.Data.Models.Views;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImageStock.Controllers
{
    public class ProfileController : Controller
    {
        private AppDbContext _appDbContext;
        private IWebHostEnvironment _env;
        public string AvatarsDirectory;
        public ProfileController(IWebHostEnvironment env, AppDbContext appDbContext)
        {
            _env = env;
            _appDbContext = appDbContext;
            AvatarsDirectory = Path.Combine(env.WebRootPath, "storage\\avatars");
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
            ViewResult viewResult;
            if (ModelState.IsValid)
            {
                UserProfile userProfile = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == data.Username);
                if (userProfile == null)
                {

                    userProfile = new UserProfile {
                        Username = data.Username,
                        PwdHash = data.Password,
                        AvatarUrl = await TryGetAvatar(data)
                    };
                    try
                    {
                        await _appDbContext.Users.AddAsync(userProfile);
                        await _appDbContext.SaveChangesAsync();
                        return View("RegisterSuccess", userProfile);
                    }
                    catch (Exception)
                    {
                        viewResult = View("RegisterError");
                        viewResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return viewResult;
                    }
                }
                else
                {
                    ModelState.AddModelError("", $"Користувач з іменем '{data.Username}' вже зареєстрований!");
                }
            }

            viewResult = View(data);
            return viewResult;
        }

        /// <returns>Path to uploaded avatar</returns>
        private async Task<string> TryGetAvatar(RegisterFormData data)
        {
            if (data.AvatarImg == null)
                return null;
            string avatarPath = GenerateAvatarPath(data);
            using (FileStream avatarFs = System.IO.File.Open(avatarPath, FileMode.Create))
            {
                await data.AvatarImg.CopyToAsync(avatarFs);
            }

            return Path.Combine("\\", avatarPath.Replace(_env.WebRootPath, "")).Replace('\\', '/');
        }

        private string GenerateAvatarPath(RegisterFormData data)
        {
            string extension = Path.GetExtension(data.AvatarImg.FileName);
            string filename = $"avatar_{data.Username.ToLower()}";
            return Path.Combine(AvatarsDirectory, filename + extension);
        }
    }
}
