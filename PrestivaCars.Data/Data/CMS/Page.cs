using PrestivaCars.Data.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrestivaCars.Data.Data.CMS
{
    public class Page : BaseEntity
    {
        [Key]
        public int PageId { get; set; }

        [Required(ErrorMessage = "Menu title is required")]
        [MaxLength(30, ErrorMessage = "Menu title cannot exceed 30 characters")]
        [Display(Name = "Menu Title")]
        public required string LinkTitle { get; set; }

        [Required(ErrorMessage = "Page title is required")]
        [MaxLength(40, ErrorMessage = "Title can't exceed 40 characters")]
        [Display(Name = "Page title")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Slug is required")]
        [MaxLength(80)]
        [Display(Name = "URL Slug")]
        public required string Slug { get; set; }

        [Display(Name = "Short Description")]
        [MaxLength(255)]
        public string? ShortDescription { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [Column(TypeName = "nvarchar(MAX)")]
        [Display(Name = "Page Content")]
        public required string Content { get; set; }

        [Display(Name = "Show in Navigation")]
        public bool ShowInNavigation { get; set; }

        [Required]
        [Display(Name = "Display Order")]
        public int Position { get; set; }
    }
}
