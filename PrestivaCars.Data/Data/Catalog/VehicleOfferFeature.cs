using PrestivaCars.Data.Data.Catalog;
using PrestivaCars.Data.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace PrestivaCars.Data.Data.Vehicles
{
    public class VehicleOfferFeature : BaseEntity
    {
        [Key]
        public int VehicleOfferFeatureId { get; set; }

        [Required]
        [Display(Name = "Vehicle Offer")]
        public int VehicleOfferId { get; set; }

        public VehicleOffer? VehicleOffer { get; set; }

        [Required]
        [Display(Name = "Vehicle Feature")]
        public int VehicleFeatureId { get; set; }

        public VehicleFeature? VehicleFeature { get; set; }
    }
}