using System.ComponentModel.DataAnnotations;
using PrestivaCars.Data.Data.Common;

namespace PrestivaCars.Data.Data.Users
{
    public class UserProfile : BaseEntity
    {
        [Key]
        public int UserProfileId { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public string UserId { get; set; } = string.Empty;

        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [MaxLength(30)]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [MaxLength(100)]
        [Display(Name = "City")]
        public string? City { get; set; }

        [MaxLength(255)]
        [Display(Name = "Profile Image Path")]
        public string? ProfileImagePath { get; set; }
    }
}