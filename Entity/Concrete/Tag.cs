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
    [Table("Tags")]
    public class Tag : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen bu alanı boş bırakmayın.")]
        [MaxLength(50, ErrorMessage = "Maksimum uzunluk 50 karakter olmalıdır.")]
        public string Name { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public List<Article> Articles { get; set; } = new List<Article>();

    }
}
