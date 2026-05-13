using PrestivaCars.Data.Data.Common;
using PrestivaCars.Data.Data.Vehicles;
using System.ComponentModel.DataAnnotations;

namespace PrestivaCars.Data.Data.Catalog
{
    public class TransmissionType : BaseEntity
    {
        [Key]
        public int TransmissionTypeId { get; set; }

        [Required]
        [Display(Name = "Transmission type")]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string Description { get; set; } = String.Empty;

        // Relation - one to many
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}