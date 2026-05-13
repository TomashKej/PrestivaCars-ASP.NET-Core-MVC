using PrestivaCars.Data.Data.Common;
using PrestivaCars.Data.Data.Vehicles;
using System.ComponentModel.DataAnnotations;

namespace PrestivaCars.Data.Data.Catalog
{
    public class FuelType : BaseEntity
    {
        [Key]
        public int FuelTypeId { get; set; }

        [Required]
        [Display(Name = "Fuel type")]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        // relation - one to many
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}