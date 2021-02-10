using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageStock.Data.Models.Views
{
    public class UploadFormData
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Не введено заголовок допису")]
        public string Title { get; set; }
        
        [Display(Name = "Опис")]
        public string Description { get; set; }
        
        //[Required]
        [Display(Name = "Категорія")]
        public string Category { get; set; }
        
        [Required]
        [Display(Name = "Файл зображення")]
        [DataType(DataType.MultilineText)]
        public IFormFile ImgFile { get; set; }
    }
}
