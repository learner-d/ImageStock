using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageStock.Data.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PwdHash { get; set; }
        public string AvatarUrl { get; set; }
        public string GetAvatarUrl()
        {
            if (!string.IsNullOrEmpty(AvatarUrl))
                return AvatarUrl;
            return "/img/def-avatar.png";
        }
        public bool IsAdmin => Username == "Admin" || Username == "real_doer";
        public bool IsValid => Id > 0;
        public static readonly UserProfile Guest = new UserProfile 
        { 
            Id = -1, AvatarUrl = "/img/guest-avatar.png", Username = "Гість" 
        };

        public List<PostInfo> Posts { get; set; }
        public List<CommentInfo> Comments { get; set; }
    }
}
