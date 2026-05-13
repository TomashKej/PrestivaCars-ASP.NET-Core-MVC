using PrestivaCars.Data.Data.Common;
using PrestivaCars.Data.Data.Vehicles;
using System.ComponentModel.DataAnnotations;

namespace PrestivaCars.Data.Data.Catalog
{
    public class BodyType : BaseEntity
    {
        [Key]
        public int BodyTypeId { get; set; }
        
        [Required]
        [Display(Name = "Body type")]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        //relation - one to many
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}