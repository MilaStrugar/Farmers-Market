using Microsoft.EntityFrameworkCore;

namespace FarmersMarket.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<VendorCategory> VendorCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<VendorReview> VendorReviews { get; set; }

        public DbSet<Cart> Carts {get;set;}


    }
}