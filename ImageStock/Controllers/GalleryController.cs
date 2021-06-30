using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using ImageStock.Data;
using ImageStock.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using ImageStock.Utils;

namespace ImageStock.Controllers
{
    public class GalleryController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GalleryController(AppDbContext appDbContext, IWebHostEnvironment hostEnvironment)
        {
            _appDbContext = appDbContext;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View(new Gallery(_appDbContext.Posts));
        }
        public IActionResult ViewPost(int id)
        {
            PostInfo post = _appDbContext.Posts.Where(p => p.Id == id)
                .Include(p => p.Comments).ThenInclude(c => c.Writer).FirstOrDefault();
            if (post != null)
            {
                ViewData.Add("CurrentUser", _appDbContext.GetUserProfile(User));
                return PartialView(post);
            }
            return StatusCode((int)HttpStatusCode.NotFound);
        }
        public async Task<IActionResult> DeletePost(int id)
        {
            PostInfo post = await _appDbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post != null)
            {
                try
                {
                    _appDbContext.Posts.Remove(post);
                    await _appDbContext.SaveChangesAsync();
                    string imgPath = _hostEnvironment.FromVirtualPath(post.ImgUrl);
                    if (imgPath != null && System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                    return StatusCode((int)HttpStatusCode.OK);
                }
                catch (Exception)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
            }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int post_id, int author_id, string comment_text)
        {
            try
            {
                PostInfo post = await _appDbContext.Posts.FirstOrDefaultAsync(p => p.Id == post_id);
                if (post != null)
                {
                    UserProfile writerProfile = 
                        await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == author_id);
                    
                    CommentInfo comment = new CommentInfo {
                        PostId = post_id,
                        WriterId = writerProfile?.Id,
                        Text = comment_text 
                    };
                    _appDbContext.Add(comment);
                    await _appDbContext.SaveChangesAsync();

                    return StatusCode((int)HttpStatusCode.OK);
                }
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                CommentInfo comment = await _appDbContext.FindAsync<CommentInfo>(id);
                if (comment != null)
                {
                    UserProfile currentUser = _appDbContext.GetUserProfile(User);
                    if (currentUser == null || currentUser.Id != comment.WriterId && !currentUser.IsAdmin)
                        return StatusCode((int)HttpStatusCode.Forbidden);

                    else
                    {
                        _appDbContext.Remove(comment);
                        await _appDbContext.SaveChangesAsync();
                        return StatusCode((int)HttpStatusCode.OK);  
                    }
                }
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
