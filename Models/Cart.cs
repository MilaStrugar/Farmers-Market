using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FarmersMarket.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Products { get; set; }
    }
}