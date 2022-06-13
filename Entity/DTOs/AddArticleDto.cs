using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class AddArticleDto
    {
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
        public int UserId { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public int CategoryId { get; set; }

    }
}
