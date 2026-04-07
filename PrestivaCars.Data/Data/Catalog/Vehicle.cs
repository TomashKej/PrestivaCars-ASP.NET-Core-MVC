using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrestivaCars.Data.Data.Common;

namespace PrestivaCars.Data.Data.Vehicles
{
    /// <summary>
    /// The fallowing model represents a vehicle in the PrestivaCars application.
    /// It includes properties for the vehicle's brand, model, production year, mileage, fuel type, 
    /// transmission type, body type, color, engine capacity, power, VIN number, registration number, and category.
    /// </summary>
    public class Vehicle : BaseEntity
    {
        [Key]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        [MaxLength(50)]
        [Display(Name = "Brand")]
        public required string Brand { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [MaxLength(50)]
        [Display(Name = "Model")]
        public required string Model { get; set; }

        [Display(Name = "Production Year")]
        public int ProductionYear { get; set; }

        [Display(Name = "Mileage")]
        public int Mileage { get; set; }

        [MaxLength(50)]
        [Display(Name = "Fuel Type")]
        public string? FuelType { get; set; }

        [MaxLength(50)]
        [Display(Name = "Transmission")]
        public string? TransmissionType { get; set; }

        [MaxLength(50)]
        [Display(Name = "Body Type")]
        public string? BodyType { get; set; }

        [MaxLength(50)]
        [Display(Name = "Colour")]
        public string? Color { get; set; }

        [Display(Name = "Engine Capacity (cc)")]
        public int EngineCapacity { get; set; }

        [Display(Name = "Power (HP)")]
        public int PowerHp { get; set; }

        [MaxLength(50)]
        [MinLength(17)]
        [Display(Name = "VIN Number")]
        public string? Vin { get; set; }

        [MaxLength(10)]
        [Display(Name = "Registration Number")]
        public string? RegistrationNumber { get; set; }

        // Category - FK
        [Required]
        [Display(Name = "Vehicle Category")]
        public int VehicleCategoryId { get; set; }

        public VehicleCategory? VehicleCategory { get; set; }

        // VehicleOffers - relation one to many
        public ICollection<VehicleOffer>? VehicleOffers { get; set; }
    }
}