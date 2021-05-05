using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FarmersMarket.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Display(Name = "Name:")]        
        public string Name { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<VendorCategory> VendorCategories { get; set; }

    }
}