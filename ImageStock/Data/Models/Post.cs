using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageStock.Data.Models
{
    public class Post
    {
        //public int Id { get; protected set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImgUrl { get; private set; }
        public Post(string title, string descr, string imgUrl)
        {
            Title = title;
            Description = descr;
            ImgUrl = imgUrl;
        }
    }
}
