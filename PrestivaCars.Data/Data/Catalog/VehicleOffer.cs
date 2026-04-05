using PrestivaCars.Data.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrestivaCars.Data.Data.Vehicles
{
    public class VehicleOffer : BaseEntity
    {
        [Key]
        public int VehicleOfferId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100)]
        [Display(Name = "Offer Title")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Slug is required")]
        [MaxLength(120)]
        [Display(Name = "URL Slug")]
        public required string Slug { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Column(TypeName = "nvarchar(MAX)")]
        [Display(Name = "Description")]
        public required string Description { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [MaxLength(100)]
        [Display(Name = "Location")]
        public string? Location { get; set; }

        [Display(Name = "Is Featured")]
        public bool IsFeatured { get; set; }

        // Relation to Vehicle - one-to-one
        [Required]
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; }

        public Vehicle? Vehicle { get; set; }

        public ICollection<SavedOffer>? SavedOffers { get; set; }
    }
}