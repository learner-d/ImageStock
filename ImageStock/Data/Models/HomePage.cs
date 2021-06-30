using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ImageStock.Utils;

namespace ImageStock.Data.Models
{
    public class HomePage
    {
        private AppDbContext _appDbContext;

        public UserProfile GetUserProfile(ClaimsPrincipal userClaimsPrincipal) =>
            _appDbContext?.GetUserProfile(userClaimsPrincipal);

        public HomePage(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<PostInfo> GetRandomPosts(int maxCount)
        {
            var  posts = _appDbContext.Posts.RandomShuffle().Take(5);
            return posts;
        }
    }
}