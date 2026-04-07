using PrestivaCars.Data.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrestivaCars.Data.Data.Vehicles
{
    /// <summary>
    /// The following model represents a vehicle category in the PrestivaCars application.
    /// It includes properties for the category's name, slug, description, icon, and image.
    /// </summary>
    public class VehicleCategory : BaseEntity
    {
        [Key]
        public int VehicleCategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(50)]
        [Display(Name = "Category Name")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Slug is required")]
        [MaxLength(60)]
        [Display(Name = "URL Slug")]
        public required string Slug { get; set; }

        [MaxLength(255)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [MaxLength(100)]
        [Display(Name = "Icon Name")]
        public string? IconName { get; set; }

        [MaxLength(255)]
        [Display(Name = "Image Path")]
        public string? ImagePath { get; set; }

        [Required]
        [Display(Name = "Display Order")]
        public int Position { get; set; }

        // Relation to Vehicle - one-to-many
        public ICollection<Vehicle>? Vehicle { get; set; }
    }
}