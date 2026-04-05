using PrestivaCars.Data.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrestivaCars.Data.Data.Vehicles
{
    public class SavedOffer : BaseEntity
    {
        [Key]
        public int SavedOfferId { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Vehicle Offer")]
        public int VehicleOfferId { get; set; }

        // Relation to VehicleOffer - many-to-one
        public VehicleOffer? VehicleOffer { get; set; }
    }
}