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
    [Table("Articles")]
    public class Article : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen bu alanı boş bırakmayın.")]
        [MaxLength(500, ErrorMessage = "Maksimum uzunluk 500 karakter olmalıdır.")]
        public string Header { get; set; }
        [Required]
        public string Content { get; set; }
        [Required(ErrorMessage = "Lütfen bu alanı boş bırakmayın.")]
        [MaxLength(500, ErrorMessage = "Maksimum uzunluk 500 karakter olmalıdır.")]
        public string CoverPath { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public int UserId { get; set; }
        public AppUser User { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
