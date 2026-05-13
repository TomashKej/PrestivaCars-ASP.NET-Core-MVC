using System.ComponentModel.DataAnnotations;
using PrestivaCars.Data.Data.Catalog;
using PrestivaCars.Data.Data.Common;

namespace PrestivaCars.Data.Data.Vehicles
{
    /// <summary>
    /// The following model represents a vehicle in the PrestivaCars application.
    /// It includes basic vehicle data and relations to dictionary tables such as brand,
    /// fuel type, transmission type, body type, colour and vehicle category.
    /// </summary>
    public class Vehicle : BaseEntity
    {
        [Key]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [MaxLength(50)]
        [Display(Name = "Model")]
        public string Model { get; set; } = string.Empty;

        [Display(Name = "Production Year")]
        public int ProductionYear { get; set; }

        [Display(Name = "Mileage")]
        public int Mileage { get; set; }

        [Display(Name = "Engine Capacity (cc)")]
        public int EngineCapacity { get; set; }

        [Display(Name = "Power (HP)")]
        public int PowerHp { get; set; }

        [MaxLength(17)]
        [MinLength(17)]
        [Display(Name = "VIN Number")]
        public string Vin { get; set; } = string.Empty;

        [MaxLength(10)]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; } = string.Empty;


        // Category - FK
        [Range(1, int.MaxValue, ErrorMessage = "Vehicle category is required")]
        [Display(Name = "Vehicle Category")]
        public int VehicleCategoryId { get; set; }

        // VehicleCategory - relation many to one
        public VehicleCategory VehicleCategory { get; set; } = null!;


        // Brand - FK
        [Range(1, int.MaxValue, ErrorMessage = "Brand is required")]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        // Brand - relation many to one
        public Brand Brand { get; set; } = null!;


        // FuelType - FK
        [Range(1, int.MaxValue, ErrorMessage = "Fuel type is required")]
        [Display(Name = "Fuel Type")]
        public int FuelTypeId { get; set; }

        // FuelType - relation many to one
        public FuelType FuelType { get; set; } = null!;


        // TransmissionType - FK
        [Range(1, int.MaxValue, ErrorMessage = "Transmission type is required")]
        [Display(Name = "Transmission Type")]
        public int TransmissionTypeId { get; set; }

        // TransmissionType - relation many to one
        public TransmissionType TransmissionType { get; set; } = null!;


        // BodyType - FK
        [Range(1, int.MaxValue, ErrorMessage = "Body type is required")]
        [Display(Name = "Body Type")]
        public int BodyTypeId { get; set; }

        // BodyType - relation many to one
        public BodyType BodyType { get; set; } = null!;


        // VehicleColour - FK
        [Range(1, int.MaxValue, ErrorMessage = "Vehicle colour is required")]
        [Display(Name = "Vehicle Colour")]
        public int VehicleColourId { get; set; }

        // VehicleColour - relation many to one
        public VehicleColour VehicleColour { get; set; } = null!;


        // VehicleOffer - relation one to one
        // Vehicle can exist without an offer, so this relation should be optional.
        public VehicleOffer? VehicleOffer { get; set; }


        // VehicleImages - relation one to many
        public ICollection<VehicleImage> VehicleImages { get; set; } = new List<VehicleImage>();


        // SavedOffers - relation one to many
        public ICollection<SavedOffer> SavedOffers { get; set; } = new List<SavedOffer>();
    }
}