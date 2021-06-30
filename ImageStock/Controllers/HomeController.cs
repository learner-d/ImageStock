using ImageStock.Data;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            //return RedirectToAction("Users", "Admin");
            ViewData["HttpContext"] = HttpContext;
            return View(new HomePage(_appDbContext));
        }
        public IActionResult Error(int code)
        {
            return View(ErrorInfo.FromStatusCode(code));
        }
    }
}
