using System.Net;
using ImageStock.Data;
using ImageStock.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageStock.Controllers
{
    public class AdminController : Controller
    {
        private AppDbContext _appDbContext;

        public AdminController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            UserProfile userProfile = _appDbContext.GetUserProfile(User);
            if (userProfile != null && userProfile.IsAdmin)
            {
                ViewData["Users"] = _appDbContext.Users;
                return View();
            }
            else
                return StatusCode((int)HttpStatusCode.NotFound);
        }
    }
}
