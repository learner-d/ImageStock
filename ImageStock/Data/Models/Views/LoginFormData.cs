using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageStock.Data.Models.Views
{
    public class LoginFormData
    {
        [Required(ErrorMessage = "Відсутнє ім'я користувача")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Відсутній пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
