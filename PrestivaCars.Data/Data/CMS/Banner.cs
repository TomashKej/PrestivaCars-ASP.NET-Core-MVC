using System.ComponentModel.DataAnnotations;
using PrestivaCars.Data.Data.Common;

namespace PrestivaCars.Data.Data.CMS
{
    /// <summary>
    /// Represents a banner entity for the CMS, containing properties for title, subtitle, button text, URL, image path, and display order.
    /// </summary>
    public class Banner : BaseEntity
    {
        [Key]
        public int BannerId { get; set; }

        [Required(ErrorMessage = "Placement key is required")]
        [MaxLength(80)]
        [Display(Name = "Placement Key")]
        public string PlacementKey { get; set; } = string.Empty;

        [MaxLength(80)]
        [Display(Name = "Company Label")]
        public string CompanyNameLabel { get; set; } = string.Empty;

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