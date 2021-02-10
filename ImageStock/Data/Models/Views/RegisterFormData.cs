using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageStock.Data.Models.Views
{
    public class RegisterFormData
    {
        [Display(Name = "Ім'я користувача")]
        [Required(ErrorMessage = "Відсутнє ім'я користувача")]
        public string Username { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Відсутній пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Повторіть пароль")]
        [Required(ErrorMessage = "Необхідно повторити пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не збігаються")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Аватар користувача")]
        public IFormFile AvatarImg { get; set; }
    }
}
