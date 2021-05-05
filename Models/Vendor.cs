using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmersMarket.Models
{
    public class Vendor
    {
        [Key]

        public int VendorId { get; set; }
        [Required]
        [Display(Name = "Vendor Name:")]

        public string VendorName { get; set; }
        [Required]
        [Display(Name = "Owner:")]
        public string VendorOwner { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Vendor email:")]
        public string VendorEmail { get; set; }
        [Required]
        [Display(Name = "Vendor website:")]
        public string VendorSite { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Vendor phone number:")]
        public string VendorPhone { get; set; }
        [Required]
        [Display(Name = "Vendor location:")]
        public string VendorLocation { get; set; }
        [Required]
        [Display(Name = "Vendor description:")]
        public string VendorDescription { get; set; }
        [Required]
        [Display(Name = "Date vendor joined:")]

        public DateTime VendorJoinDate { get; set; }
        [Display(Name = "Image:")]
        public string Image { get; set; }

        //Relationships
        public List<Product> Products { get; set; }
        public List<VendorReview> VendorReviews { get; set; }
        public List<VendorCategory> VendorCategories { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}