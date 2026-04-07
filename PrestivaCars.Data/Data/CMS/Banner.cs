using System.ComponentModel.DataAnnotations;
using PrestivaCars.Data.Data.Common;

namespace PrestivaCars.Data.Data.Cms
{
    public class Banner : BaseEntity
    {
        [Key]
        public int BannerId { get; set; }

        [Required(ErrorMessage = "Banner title is required")]
        [MaxLength(100)]
        [Display(Name = "Banner Title")]
        public required string Title { get; set; }

        [MaxLength(200)]
        [Display(Name = "Subtitle")]
        public string? Subtitle { get; set; }

        [MaxLength(50)]
        [Display(Name = "Button Text")]
        public string? ButtonText { get; set; }

        [MaxLength(255)]
        [Display(Name = "Button URL")]
        public string? ButtonUrl { get; set; }

        [MaxLength(255)]
        [Display(Name = "Image Path")]
        public string? ImagePath { get; set; }

        [Display(Name = "Display Order")]
        public int Position { get; set; }
    }
}