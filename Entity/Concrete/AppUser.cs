using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class AppUser : IdentityUser<int>, IEntity
    {
        [Required(ErrorMessage = "Lütfen bu alanı boş bırakmayın.")]
        [MaxLength(50, ErrorMessage = "Maksimum uzunluk 50 karakter olmalıdır.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lütfen bu alanı boş bırakmayın.")]
        [MaxLength(50, ErrorMessage = "Maksimum uzunluk 50 karakter olmalıdır.")]
        public string LastName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
