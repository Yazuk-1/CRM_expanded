using System.ComponentModel.DataAnnotations;

namespace RealEstateCRM.Models
{
    public class Client
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

        // Example: Preferences for property search (budget, property type, etc.)
        public string Preferences { get; set; }
    }
}
