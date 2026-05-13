using PrestivaCars.Data.Data.Common;
using PrestivaCars.Data.Data.Vehicles;
using System.ComponentModel.DataAnnotations;

namespace PrestivaCars.Data.Data.Catalog
{
    public class VehicleColour : BaseEntity
    {
        [Key]
        public int VehicleColourId { get; set; }

        [Required(ErrorMessage = "Colour name is required!")]
        [MaxLength(25)]
        [Display(Name = "Colour name")]
        public required string ColourName { get; set; }

        // Relations 
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
