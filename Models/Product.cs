using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FarmersMarket.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Product name:")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Description:")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Price:")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Quantity:")]
        public int Quantity { get; set; }
        [Display(Name = "Image:")]
        public string Image { get; set; }
        //Relationships
        [Required]
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public List<ProductReview> ProductReview { get; set; }
        public List<ProductCategory> ProductCategory { get; set; }

        public List<Cart> ProductCart { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}