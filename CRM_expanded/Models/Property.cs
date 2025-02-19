using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateCRM.Models
{
    public class Property
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Listing Date")]
        public DateTime ListingDate { get; set; }

        [Display(Name = "Property Type")]
        public string PropertyType { get; set; }

        [Display(Name = "Bedrooms")]
        public int NumberOfBedrooms { get; set; }

        [Display(Name = "Bathrooms")]
        public int NumberOfBathrooms { get; set; }

        [Display(Name = "Square Feet")]
        public int SquareFeet { get; set; }
    }
}
