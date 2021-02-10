using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ImageStock.Data;
using ImageStock.Data.Models;
using ImageStock.Data.Models.Views;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ImageStock.Controllers
{
    public class UploadController : Controller
    {
        private IWebHostEnvironment _webHostEnvironment;
        private AppDbContext _appDbContext;

        public UploadController(IWebHostEnvironment webHostEnvironment, AppDbContext appDbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UploadFormData data)
        {
            if (ModelState.IsValid)
            {
                string path = GenerateImgPath(Path.GetExtension(data.ImgFile.FileName));

                bool successWritten = false;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await data.ImgFile.CopyToAsync(fileStream);
                    successWritten = true;
                }

                if (successWritten)
                {
                    try
                    {
                        PostInfo post = new PostInfo
                        {
                            Title = data.Title,
                            Author = _appDbContext.GetUserProfile(User),
                            ImgUrl = ConvertPath(path),
                            Description = data.Description
                        };

                        await _appDbContext.Posts.AddAsync(post);
                        await _appDbContext.SaveChangesAsync();
                        return StatusCode((int)HttpStatusCode.OK);
                    }
                    catch (Exception)
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError);
                    }
                }
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        private string GenerateImgPath(string imgExtension)
        {
            string directory = _webHostEnvironment.WebRootPath + "\\storage\\images\\";
            char[] filename = new char[16];

            string path = null;
            do
            {
                Random rnd = new Random();
                for (int i = 0; i < filename.Length; i++)
                {
                    filename[i] = Convert.ToChar(rnd.Next(Convert.ToInt32('a'), Convert.ToInt32('z') + 1));
                }
                path = Path.Combine(directory, new string(filename) + imgExtension);

            }
            while (System.IO.File.Exists(path));
            return path;
        }

        private string ConvertPath(string path)
        {
            StringBuilder str = new StringBuilder(path);
            str.Replace(_webHostEnvironment.WebRootPath, "~");
            str.Replace('\\', '/');
            return str.ToString();
        }
    }
}
