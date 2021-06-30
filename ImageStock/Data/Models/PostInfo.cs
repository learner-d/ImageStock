using System.Collections.Generic;

namespace ImageStock.Data.Models
{
    public class PostInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }

        public int? AuthorId { get; set; }
        public UserProfile Author { get; set; }

        public List<CommentInfo> Comments { get; set; }
    }
}
