using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Category
{
    public class CategoryViewModel
    {
        [Display(Name="Kategori adı giriniz")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Kategori açıklama giriniz")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Kategori resmi giriniz")]
        public string ImageUrl { get; set; }

        [Display(Name = "Üst kategorisini giriniz")]
        [Required]
        public int ParentId { get; set; }

        [Display(Name = "Aktif mi")]
        [Required]
        public bool IsActive { get; set; }

    }
}
