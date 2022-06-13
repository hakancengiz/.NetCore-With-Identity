using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    [Table("Categories")]
    public class Category : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen bu alanı boş bırakmayın.")]
        [MaxLength(100, ErrorMessage = "Maksimum uzunluk 100 karakter olmalıdır.")]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
