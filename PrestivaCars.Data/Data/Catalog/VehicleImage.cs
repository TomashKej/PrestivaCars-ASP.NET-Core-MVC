using PrestivaCars.Data.Data.Common;
using PrestivaCars.Data.Data.Vehicles;
using System.ComponentModel.DataAnnotations;

namespace PrestivaCars.Data.Data.Catalog
{
    /// <summary>
    /// This model represents an image associated with a vehicle offer in the PrestivaCars application.
    /// It includes properties for the image's path, alt text, display order, whether it is the main image, and the associated vehicle offer ID.
    /// </summary>
    public class VehicleImage : BaseEntity
    {
        [Key]
        public int VehicleImageId { get; set; }

        [Required(ErrorMessage = "Image path is required")]
        [MaxLength(255)]
        [Display(Name = "Location of the image")]
        public required string ImagePath { get; set; }

        [MaxLength(100)]
        [Display(Name = "Alt text")]
        public string? AltText { get; set; }

        [Display(Name = "Display Order")]
        public int Position { get; set; }

        [Display(Name = "Is Main Image")]
        public bool IsMain { get; set; }

        [Required]
        [Display(Name = "Vehicle Offer")]
        public int VehicleOfferId { get; set; }

        public VehicleOffer? VehicleOffer { get; set; }

    }
}
