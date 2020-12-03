using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageStock.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImageStock.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGallery gallery;
        public GalleryController(IGallery _gallery)
        {
            gallery = _gallery;
        }
        public IActionResult Index()
        {
            return View(gallery);
        }
    }
}
