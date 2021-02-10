using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageStock.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ImageStock.Data.Models;

namespace ImageStock.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _appDbContext;
        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            //AppDbContext.SetupDefaults(appDbContext);
        }
        //[Authorize]
        public IActionResult Index()
        {
            //return RedirectToAction("Users", "Admin");
            ViewData["HttpContext"] = HttpContext;
            return View(_appDbContext);
        }
    }
}
