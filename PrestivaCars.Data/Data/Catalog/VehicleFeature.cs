using PrestivaCars.Data.Data.Common;
using PrestivaCars.Data.Data.Vehicles;
using System.ComponentModel.DataAnnotations;

namespace PrestivaCars.Data.Data.Catalog
{
    public class VehicleFeature : BaseEntity
    {
        [Key]
        public int VehicleFeatureId { get; set; }

        [Required(ErrorMessage = "Feature name is required")]
        [MaxLength(100)]
        [Display(Name = "Feature name")]
        public required string Name { get; set; }

        [MaxLength(100)]
        [Display(Name = "Icon name")]
        public string? IconName { get; set; }

        [Display(Name = "Display oreder")]
        public int Position { get; set; }

        // Relation to VehicleOfferFeature - one-to-many
        public ICollection<VehicleOfferFeature>? VehicleOfferFeatures { get; set; }
    }
}
