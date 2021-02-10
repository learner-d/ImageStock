using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageStock.Utils
{
    public static class UtilsExt
    {
        public static string FromVirtualPath(this IWebHostEnvironment env, string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                StringBuilder str = new StringBuilder(path);
                str.Replace("~", env.WebRootPath);
                str.Replace("/", "\\");
                return str.ToString();
            }
            return null;
        }
    }
}
