namespace ImageStock.Data.Models
{
    public class CommentInfo
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int? WriterId { get; set; }
        public UserProfile Writer { get; set; }

        public int PostId { get; set; }
        public PostInfo Post { get; set; }
    }; 
}