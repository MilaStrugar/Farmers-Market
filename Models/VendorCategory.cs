using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FarmersMarket.Models
{
    public class VendorCategory
    {
        public int VendorCategoryId { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendors { get; set; }
    }
}