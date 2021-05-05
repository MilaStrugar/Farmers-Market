using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FarmersMarket.Models
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        [Required]
        public int CategoryId { get; set; }


        public Category Category { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}