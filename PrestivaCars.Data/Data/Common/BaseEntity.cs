using System.ComponentModel.DataAnnotations;

namespace PrestivaCars.Data.Data.Common
{
    /// <summary>
    /// Base entity is an abstract class that includes common properties for all entities in the PrestivaCars application.
    /// It includes properties for tracking the active status, creation, update, and deletion information of the entities.
    /// This base class can be inherited by other entity models to ensure consistency and reduce code duplication across the application.
    /// </summary>
    public abstract class BaseEntity
    {
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; } = string.Empty;

        [ScaffoldColumn(false)]
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; } = string.Empty;

        [ScaffoldColumn(false)]
        [Display(Name = "Deleted At")]
        public DateTime? DeletedAt { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Deleted By")]
        public string DeletedBy { get; set; } = string.Empty;
    }
}