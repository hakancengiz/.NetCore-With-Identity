using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Lütfen e-posta adresi giriniz.")]
        [Display(Name = "E-Posta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen parolanızı giriniz.")]
        [Display(Name = "Parola")]
        public string Password { get; set; }
    }
}
