using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FarmersMarket.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$", ErrorMessage = "Password must contain 1 letter, 1 number and 1 special character and must be at least 8 characters long")]
        [MinLength(8, ErrorMessage = "Password needs to be longer then 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords need to match")]
        [Display(Name = "Confirm password:")]
        public string ConfirmPassword { get; set; }

        //Relationships
        public List<Cart> Cart { get; set; }
        public List<ProductReview> MyProductReviews { get; set; }
        public List<VendorReview> MyVendorReviews { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}