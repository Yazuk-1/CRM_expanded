using System.ComponentModel.DataAnnotations;

namespace RealEstateCRM.Models
{
    public class Agent
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        // Example: License number for verification purposes
        [Display(Name = "License Number")]
        public string LicenseNumber { get; set; }
    }
}
