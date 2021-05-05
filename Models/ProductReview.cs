using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FarmersMarket.Models
{
    public class ProductReview
    {
        [Key]
        public int ProductReviewId { get; set; }
        [Required]
        public int ReviewId { get; set; }
        public Review Review { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}