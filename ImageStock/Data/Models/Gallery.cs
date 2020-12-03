using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageStock.Data.Models
{
    public class Gallery
    {
        protected List<Post> posts;
        public IReadOnlyList<Post> Posts => posts;
    }
}
