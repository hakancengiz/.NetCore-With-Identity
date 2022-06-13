using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Lütfen adınızı giriniz.")]
        [Display(Name = "Adınız")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı giriniz.")]
        [Display(Name = "Soyadınız")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Lütfen e-posta adresi giriniz.")]
        [Display(Name = "E-Posta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen parolanızı giriniz.")]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen parolanızı giriniz.")]
        [Display(Name = "Parola")]
        [Compare("Password", ErrorMessage = "Parolalar uyuşmuyor.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Lütfen rol seçiniz.")]
        [Display(Name = "Kullanıcı Rolü")]
        public string UserRole { get; set; }
    }
}
