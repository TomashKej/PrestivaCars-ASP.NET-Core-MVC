using System.ComponentModel.DataAnnotations;

namespace PrestivaCars.Data.Data.Common
{
    public abstract class BaseEntity
    {
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; } = string.Empty;

        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; } = string.Empty;

        [Display(Name = "Deleted At")]
        public DateTime? DeletedAt { get; set; }

        [Display(Name = "Deleted By")]
        public string DeletedBy { get; set; } = string.Empty;
    }
}