using PrestivaCars.Data.Data.Common;
using PrestivaCars.Data.Data.Vehicles;
using System.ComponentModel.DataAnnotations;

namespace PrestivaCars.Data.Data.Catalog
{
    public class Brand : BaseEntity
    {
        [Key]
        public int BrandId { get; set; }

        [Required]
        [Display(Name = "Brand name")]
        [MaxLength(40)]
        public required string Name { get; set; }

        [Display(Name = "Notes")]
        public string Description { get; set; } = string.Empty;

        // Relations - one to many
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}