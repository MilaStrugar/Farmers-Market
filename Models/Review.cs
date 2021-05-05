using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FarmersMarket.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        [Required]
        [Display(Name = "Review:")]
        public string ReviewMessage { get; set; }
        [Display(Name = "Image:")]
        public string ReviewImage { get; set; }
        [Required]
        [Display(Name = "Rating:")]
        public int Rating { get; set; }
        //Relationships
        public int UserId { get; set; }
        public User Reviewer { get; set; }
        public List<VendorReview> VendorReviews { get; set; }
        public List<ProductReview> ProductReviews { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}