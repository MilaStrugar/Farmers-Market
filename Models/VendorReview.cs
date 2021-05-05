using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FarmersMarket.Models
{
    public class VendorReview
    {
        [Key]
        public int VendorReviewId { get; set; }
        [Required]
        public int ReviewId { get; set; }
        // [Required(ErrorMessage="Vendor Review is required: Please enter Review!")]
        // [Display(Name = "Review: ")]
        public Review Review { get; set; }
        [Required]
        
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }

    }
}