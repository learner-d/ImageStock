using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageStock.Data.Models
{
    public class Gallery
    {
        protected List<PostInfo> _posts;
        public IReadOnlyList<PostInfo> Posts => _posts;
        public Gallery(IQueryable<PostInfo> posts)
        {
            _posts = posts.ToList();
        }
    }
}
